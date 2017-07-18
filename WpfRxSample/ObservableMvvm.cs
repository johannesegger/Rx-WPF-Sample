using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;

namespace WpfRxSample
{
    public static class ViewModelExtensions
    {
        public static IObservable<TProperty> Changed<TObj, TProperty>(
            this TObj obj,
            Expression<Func<TObj, TProperty>> propertyExpr)
        {
            return GetPropertyExpressions((MemberExpression)propertyExpr.Body)
                .Aggregate(
                    Observable.Return<object>(obj),
                    (viewModelObservable, expr) => viewModelObservable
                        .Select(o =>
                        {
                            if (o == null)
                            {
                                return Observable.Return(GetDefaultValue(expr.ReturnType));
                            }
                            else
                            {
                                var getValue = expr.Compile();
                                if (o is INotifyPropertyChanged inpc)
                                {
                                    return GetPropertyObservableFromInpcViewModel(
                                        inpc,
                                        ((MemberExpression)expr.Body).Member.Name,
                                        getValue);
                                }
                                else
                                {
                                    return Observable.Return(getValue.DynamicInvoke(o));
                                }
                            }
                        })
                        .Switch(),
                    o => o.Cast<TProperty>())
                .DistinctUntilChanged(); // TODO use reference equality
        }

        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private static IObservable<object> GetPropertyObservableFromInpcViewModel(
            INotifyPropertyChanged viewModel,
            string propertyName,
            Delegate getValue)
        {
            return Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => viewModel.PropertyChanged += h,
                    h => viewModel.PropertyChanged -= h)
                .Where(p => p.EventArgs.PropertyName.Equals(propertyName, StringComparison.Ordinal))
                .Select(p => p.Sender)
                .StartWith(viewModel)
                .Select(p => getValue.DynamicInvoke(p));
        }

        private static IEnumerable<LambdaExpression> GetPropertyExpressions(MemberExpression expr)
        {
            if (expr == null)
            {
                yield break;
            }

            foreach (var parentExpression in GetPropertyExpressions(expr.Expression as MemberExpression))
            {
                yield return parentExpression;
            }

            var param = Expression.Parameter(expr.Expression.Type, "p");
            yield return Expression.Lambda(Expression.Property(param, (PropertyInfo)expr.Member), param);
        }

        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Func<TProperty1, TProperty2, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                combine);
        }
    }
}

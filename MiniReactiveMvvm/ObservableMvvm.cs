using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;

namespace MiniReactiveMvvm
{
    public static class ViewModelExtensions
    {
        public static IObservable<TProperty> Changed<TObj, TProperty>(
            this TObj obj,
            Expression<Func<TObj, TProperty>> propertyExpr)
        {
            return GetPropertyExpressions((MemberExpression)propertyExpr.Body)
                .Aggregate(
                    Observable.Return<object?>(obj),
                    ObserveProperty,
                .DistinctUntilChanged(EqualityComparer<TProperty>.Default);
        }

        private static IObservable<object?> ObserveProperty(IObservable<object?> viewModelObservable, LambdaExpression expr)
        {
            return viewModelObservable
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
                .Switch();
        }

        private static object? GetDefaultValue(Type type)
        {
            return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
        }

        private static IObservable<object?> GetPropertyObservableFromInpcViewModel(
            INotifyPropertyChanged viewModel,
            string propertyName,
            Delegate getValue)
        {
            return Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => viewModel.PropertyChanged += h,
                    h => viewModel.PropertyChanged -= h)
                .Where(p => p.EventArgs.PropertyName == null || p.EventArgs.PropertyName.Equals(propertyName, StringComparison.Ordinal))
                .Select(p => p.Sender)
                .StartWith(viewModel)
                .Select(p => getValue.DynamicInvoke(p));
        }

        private static IEnumerable<LambdaExpression> GetPropertyExpressions(MemberExpression? expr)
        {
            if (expr == null)
            {
                yield break;
            }

            if (expr.Expression is MemberExpression parentExpression)
            {
                foreach (var ancestorExpression in GetPropertyExpressions(parentExpression))
                {
                    yield return ancestorExpression;
                }
            }
            else if (expr.Expression is ParameterExpression parameterExpression)
            {
                var param = Expression.Parameter(parameterExpression.Type, "p");
                yield return Expression.Lambda(Expression.Property(param, (PropertyInfo)expr.Member), param);
            }
            else
            {
                throw new Exception($"Expected \"{expr.Expression}\" to be either a {nameof(MemberExpression)} or a {nameof(ParameterExpression)}");
            }
        }
    }
}

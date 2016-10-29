using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace WpfApplication1
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
        {
            if (Equals(field, value)) return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class ViewModelExtensions
    {
        public static IObservable<TProperty> Changed<TViewModel, TProperty>(
            this TViewModel viewModel,
            Expression<Func<TViewModel, TProperty>> propertyExpr)
            where TViewModel : INotifyPropertyChanged
        {
            var propertyName = propertyExpr
                .Body
                .DirectCast<MemberExpression>()
                .Member
                .Name;
            var getProperty = propertyExpr.Compile();
            return Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => viewModel.PropertyChanged += h,
                    h => viewModel.PropertyChanged -= h
                )
                .Where(p => p.EventArgs.PropertyName == propertyName)
                .Select(p => (TViewModel)p.Sender)
                .StartWith(viewModel)
                .Select(getProperty);
        }

        public static T DisposeWith<T>(this T disposable, CompositeDisposable container)
            where T : IDisposable
        {
            container.Add(disposable);
            return disposable;
        }
    }
}

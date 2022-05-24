using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace MiniReactiveMvvm
{
    internal static class DisposableExtensions
    {
        public static T DisposeWith<T>(this T disposable, CompositeDisposable container)
            where T : IDisposable
        {
            container.Add(disposable);
            return disposable;
        }

        public static IObservable<T> Connect<T>(this IConnectableObservable<T> o, CompositeDisposable d)
        {
            d.Add(o.Connect());
            return o;
        }
    }
}

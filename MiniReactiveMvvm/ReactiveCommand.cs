using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniReactiveMvvm
{
    public class ReactiveCommand<TIn, TOut> : ICommand, IObservable<TOut>, IDisposable
    {
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        private readonly ISubject<int> executionCountSubject = new BehaviorSubject<int>(0);
        private readonly ISubject<TIn, TOut> executeSubject;

        private bool canExecute = true;

        public ReactiveCommand(
            IObservable<bool> canExecuteObservable,
            ISubject<TIn, TOut> executeSubject,
            IScheduler scheduler)
        {
            canExecuteObservable
                .CombineLatest(
                    executionCountSubject.Scan((a, b) => a + b),
                    (canExec, isExec) => isExec == 0 ? canExec : false)
                .ObserveOn(scheduler)
                .Subscribe(p =>
                {
                    canExecute = p;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                })
                .DisposeWith(disposable);

            this.executeSubject = Subject.Create(
                executeSubject,
                executeSubject.Publish().Connect(disposable));

            this.executeSubject
                .Subscribe(
                    _ => executionCountSubject.OnNext(-1),
                    _ => executionCountSubject.OnNext(-1),
                    () => executionCountSubject.OnNext(-1))
                .DisposeWith(disposable);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            executionCountSubject.OnNext(1);
            executeSubject.OnNext((TIn)parameter);
        }

        public IDisposable Subscribe(IObserver<TOut> observer)
        {
            return executeSubject.Subscribe(observer);
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }

    public static class ReactiveCommand
    {
        public static ReactiveCommand<TIn, TOut> Create<TIn, TOut>(
            IObservable<bool> canExecute,
            Func<TIn, CancellationToken, Task<TOut>> execute,
            IScheduler scheduler)
        {
            var executeSubject = new Subject<TIn>();
            var resultObservable = executeSubject
                .Select(p => Observable.FromAsync(ct => execute(p, ct)))
                .Switch();

            return new ReactiveCommand<TIn, TOut>(
                canExecute,
                Subject.Create(executeSubject, resultObservable),
                scheduler);
        }
    }
}

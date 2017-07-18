using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfRxSample
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class MainViewModel : IDisposable
    {
        private readonly CompositeDisposable _Disposable = new CompositeDisposable();

        public string SearchText { get; set; }

        public ImmutableList<Person> Persons { get; set; } = ImmutableList<Person>.Empty;

        public double AverageAge { get; set; }

        public ImmutableStack<LogMessage> LogMessages { get; set; } = ImmutableStack<LogMessage>.Empty;

        public double ZoomFactor { get; set; } = 1;

        public MainViewModel()
        {
            this.Changed(p => p.SearchText) // Whenever this property changes
                .Throttle(TimeSpan.FromMilliseconds(500)) // Wait until there were no changes for 500 ms
                .Select(text =>
                {
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        // No search string, no results
                        return Observable.Return(ImmutableList<Person>.Empty);
                    }
                    else
                    {
                        // Convert a task to an observable.
                        // The nice thing here is that we get a cancellation token.
                        // Cancellation of this token is requested when a new event occurs.
                        return Observable.FromAsync(ct => Search(text, ct));
                    }
                })
                // Because we now have an IObservable<IObservable<...>> we can use switch
                // to switch to the latest inner IObservable and only listen to changes of the latest inner IObservable.
                // As soon as a new inner IObservable arrives cancellation of the cancellation token from above
                // is automatically requested, so `ct.IsCancellationRequested` is `true`.
                .Switch()
                // ViewModel properties should always be set on the WPF thread (I think)
                .ObserveOnDispatcher()
                // Subscribing to an IObservable means that we register a listener.
                // So without subscribing nothing happens (i.e. no search occurs)
                // Subscribe also returns an IDisposable that when disposed deregisters all resources (event handlers, timers, etc.)
                .Subscribe(persons => Persons = persons)
                .DisposeWith(_Disposable);

            this.Changed(p => p.Persons)
                .Select(persons =>
                {
                    if (persons.IsEmpty)
                    {
                        return Observable.Return(0.0); // Average age is 0 when there are no persons
                    }
                    return persons
                        .Select(person => person.Changed(p => p.Age)) // Get IObservables for the ages of all persons
                        .CombineLatest() // Combine the latest values of those observables
                        .Select(ages => ages.Average()); // Get the average age
                })
                .Switch() // Switch to the latest inner observable from the select statement above
                .Subscribe(avgAge => AverageAge = avgAge) // Set the property
                .DisposeWith(_Disposable);
        }

        private readonly ConcurrentQueue<Color> colorQueue =
            new ConcurrentQueue<Color>(new[] { Colors.RosyBrown, Colors.PowderBlue, Colors.SpringGreen });
        private static readonly Random rand = new Random();
        private async Task<ImmutableList<Person>> Search(string text, CancellationToken ct)
        {
            using (GetColor(out var color))
            {
                try
                {
                    Log($"Starting search for \"{text}\"", color, Colors.Black);
                    await Task.Delay(TimeSpan.FromSeconds(2), ct); // Simulate work
                    Log($"Finished search for \"{text}\"", color, Colors.Green);
                    return Enumerable.Range(0, rand.Next(5, 10))
                        .Select(i => new Person($"{text} {i + 1}", rand.Next(20, 50)))
                        .ToImmutableList();
                }
                catch (OperationCanceledException)
                {
                    Log($"Cancelled search for \"{text}\"", color, Colors.Red);
                    throw;
                }
            }
        }

        private IDisposable GetColor(out Color color)
        {
            if (!colorQueue.TryDequeue(out var result))
            {
                color = Colors.Black;
                return Disposable.Empty;
            }
            color = result;
            return Disposable.Create(() => colorQueue.Enqueue(result));
        }

        private void Log(string message, Color backgroundColor, Color foregroundColor)
        {
            LogMessages = LogMessages
                .Push(new LogMessage(message, backgroundColor, foregroundColor));
        }

        public void Dispose()
        {
            _Disposable.Dispose();
        }
    }

    public class LogMessage
    {
        public DateTime Time { get; } = DateTime.Now;
        public string Message { get; }
        public Color BackgroundColor { get; }
        public Color ForegroundColor { get; }

        public LogMessage(string message, Color backgroundColor, Color foregroundColor)
        {
            Message = message;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }
    }

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Person
    {
        public string Name { get; }

        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
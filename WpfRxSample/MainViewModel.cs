using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class MainViewModel : ViewModel, IDisposable
    {
        private readonly CompositeDisposable _Disposable = new CompositeDisposable();

        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { SetProperty(ref _SearchText, value); }
        }

        private ImmutableList<Person> _Persons = ImmutableList<Person>.Empty;
        public ImmutableList<Person> Persons
        {
            get { return _Persons; }
            set { SetProperty(ref _Persons, value); }
        }

        private double _AverageAge;
        public double AverageAge
        {
            get { return _AverageAge; }
            set { SetProperty(ref _AverageAge, value); }
        }

        private ImmutableList<LogMessage> _LogMessages = ImmutableList<LogMessage>.Empty;
        public ImmutableList<LogMessage> LogMessages
        {
            get { return _LogMessages; }
            private set { SetProperty(ref _LogMessages, value); }
        }

        public MainViewModel()
        {
            this.Changed(p => p.SearchText) // Whenever this property changes
                .Skip(1) // Don't search for initial empty string
                .Throttle(TimeSpan.FromMilliseconds(500)) // Ignore subsequent changes that occur within 500 ms
                .Select(text =>
                    // Convert a task to an observable.
                    // The nice thing here is that we get a cancellation token.
                    // This token is cancelled when a new event occurs
                    Observable.FromAsync(ct => Search(text, ct))
                )
                // Because we now have an IObservable<IObservable<...>> we can use switch
                // to switch to the latest inner IObservable and only listen to changes of the latest inner IObservable.
                // As soon as a new inner IObservable arrives the cancellation token from above
                // is automatically cancelled.
                .Switch()
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

        private async Task<ImmutableList<Person>> Search(string text, CancellationToken ct)
        {
            try
            {
                Log($"Starting search for {text}");
                
                await Task.Delay(TimeSpan.FromSeconds(2), ct); // Simulate work
                var rand = new Random();
                return Enumerable.Range(0, rand.Next(1, 30))
                    .Select(i => new Person($"{text} {i + 1}", rand.Next(20, 50)))
                    .ToImmutableList();
            }
            catch(OperationCanceledException)
            {
                Log($"Cancelled search for {text}");
                throw;
            }
            finally
            {
                Log($"Finished search for {text}");
            }
        }

        private void Log(string message)
        {
            LogMessages = ImmutableList<LogMessage>.Empty
                    .Add(new LogMessage(message))
                    .AddRange(LogMessages);
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

        public LogMessage(string message)
        {
            Message = message;
        }
    }

    public class Person : ViewModel
    {
        public string Name { get; }

        private int _Age;
        public int Age
        {
            get { return _Age; }
            set { SetProperty(ref _Age, value); }
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
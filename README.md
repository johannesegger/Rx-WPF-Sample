# Rx-WPF-Sample
A hopefully compelling example to use Reactive Extensions in WPF projects.

## Run
* Sample application: `dotnet run --project WpfRxSample`
* Tests: `dotnet test Rx.Test`

## What is Reactive Programming
The term reactive sounds a lot like it's got to do with handling events, right? That's true, but Reactive Programming is much more. It's about representing any changes that can happen within a system. And basically every system is constantly changing, is it a text box value, some database entries or just a simple object property.

There are [many](http://introtorx.com/) [nice](https://gist.github.com/staltz/868e7e9bc2a7b8c1f754) [introductions](http://reactivex.io/intro.html) [to](https://cycle.js.org/streams.html) Reactive Programming, so I just give you a short overview what Reactive Programming looks like as a .NET developer.

### Reactive Programming in .NET
There are two interfaces in the .NET BCL that are heavily used for Reactive Programming: `System.IObservable<T>` and `System.IObserver<T>`.

```csharp
public interface IObservable<out T>
{
    IDisposable Subscribe(IObserver<T> observer);
}

public interface IObserver<in T>
{
    void OnNext(T value);
    void OnError(Exception error);
    void OnCompleted();
}
```

`IObservable<T>` represents data that can change over time (e.g. sensor values, mouse location) or events that occured (e.g. button click, timer elapsed).  
`IObserver<T>` is the sink for this data (`OnNext` is like an event handler).

An observer can _subscribe_ to and _unsubscribe_ from (using the returned `IDisposable`) an observable. While subscribed the observable _notifies_ the observer about new data (`OnNext`) and about the termination of the observable sequence (both successful (`OnCompleted`) or due to an error (`OnError`)).

### What's the difference to .NET events?
We previously noticed that `IObserver<T>.OnNext` is like an event handler, so why can't we do that with plain old .NET events? Because .NET events are missing a type for the _stream_ of events, which means we can't pass e.g. `INotifyPropertyChanged.PropertyChanged` around to filter its data or combine it with other events before adding a handler to it.

### Similarities between `IObservable<T>` and `IEnumerable<T>`
Being a .NET developer you most likely know `IEnumerable<T>` which represents a sequence of data. It declares a single method `IEnumerable<T>.GetEnumerator`, but still instances of it can be filtered, combined and more. It's the same with `IObservable<T>`. Although it only declares a single method `IObservable<T>.Subscribe` it enables sophisticated operations on it. The key here is the special implementations of these interfaces. They wrap zero or more inner instances and use the data of these inner instances to produce their own data. E.g. a very rudimentary implementation of a filter operator might look like this:

```csharp
/// <summary>
/// Filter data from an underlying observable sequence using a filter function.
/// </summary>
internal class FilterObservable<T> : IObservable<T>
{
    private readonly IObservable<T> _inner;
    private readonly Func<T, bool> _filter;

    public FilterObservable(IObservable<T> inner, Func<T, bool> filter)
    {
        _inner = inner;
        _filter = filter;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        // For simplicity we assume that we have an overload of
        // `IObservable<T>.Subscribe` that can construct an observer
        // using the three required functions.
        return _inner
            .Subscribe(
                onNext: data =>
                {
                    if (_filter(data))
                    {
                        observer.OnNext(data);
                    }
                },
                onError: observer.OnError,
                onCompleted: observer.OnCompleted);
    }
}

public static class ObservableExtensions
{
    /// <summary>
    /// Extension method to enable operator chains.
    /// Example usage: <code>observable.Filter(i => i > 10);</code>.
    /// </summary>
    public static IObservable<T> Filter<T>(this IObservable<T> o, Func<T, bool> filter)
    {
        return new FilterObservable<T>(o, filter);
    }
}
```

The .NET BCL only defines `System.IObservable<T>` but doesn't provide any implementations of it. It's up to library authors to implement some useful operators. In .NET there is only really [Rx.NET](https://github.com/dotnet/reactive) that defines a set of operators. Still, in a real application you will most likely implement custom operators. Rx.NET helps you a lot here. For a filter operator (which is built into Rx.NET anyway) this is trivial:

```csharp
public static class ObservableExtensions
{
    public static IObservable<T> Filter<T>(this IObservable<T> o, Func<T, bool> filter)
    {
        return Observable.Create<T>(observer =>
        {
            // This is the same as `FilterObservable<T>.Subscribe` which we saw earlier
            return o
                .Subscribe(
                    onNext: data =>
                    {
                        if (filter(data))
                        {
                            observer.OnNext(data);
                        }
                    },
                    onError: observer.OnError,
                    onCompleted: observer.OnCompleted);
        });
    }
}
```

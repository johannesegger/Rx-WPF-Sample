# Rx-WPF-Sample
A hopefully compelling example to use Reactive Extensions in WPF projects.

## Build steps
1. Restore NuGet packages: `nuget restore .`
1. Build project: `msbuild WpfRxSample.sln`

## A short introduction to Reactive Programming

The term _reactive_ sounds a lot like it's got to do with handling events, right? That's true, but _Reactive Programming_ is much more. It's about representing any changes that can happen within a system. And basically every system is constantly changing, is it a text box value, some database entries or just a simple object property. 

### Reactive Programing in .NET

There are two interfaces in the .NET BCL that are heavily used for Reactive Programming: `System.IObservable<T>` and `System.IObserver<T>`.

```
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

`IObservable<T>` represents the data that can change over time (e.g. sensor values, mouse location) or events that occured (e.g. button click, timer elapsed).  
`IObserver<T>` is the sink for this data (`OnNext` is like an event handler).

An observer can _subscribe_ to and _unsubscribe_ from (using the returned `IDisposable`) an observable. While subscribed the observable _notifies_ the observer about new data (`OnNext`) and about the termination of the observable sequence (both successful (`OnCompleted`) or due to an error (`OnError`)).

### What's the difference to .NET events?
We previously noticed that `IObserver<T>.OnNext` is like an event handler, so why not just use plain old .NET events? Because .NET events are missing a type for the _stream_ of events, which means we can't pass e.g. `INotifyPropertyChanged.PropertyChanged` around to e.g. filter its data or combine it with other events before subscribing to it.

### But wait! `IObservable<T>` defines only a single method. How can I do cool stuff like filtering data and combining observables?

In this case `IObservable<T>` is very similar to `IEnumerable<T>`. The interface itself has a very small surface, but concrete implementations of `IObservable<T>` can wrap an underlying observable and modify its data. E.g. when calling `source.Where(i => i > 10)` we wrap `source` inside a special implementation of `IObservable<T>`, that, when subscribed to, subscribes to `source` and only passes numbers greater than `10` to the subscribed observer.

The de-facto standard for Reactive Programming in .NET using `IObservable<T>` and `IObserver<T>` is [Rx.NET](https://github.com/Reactive-Extensions/Rx.NET).

## Learning resources

* [http://introtorx.com](http://introtorx.com) - Good introduction to Rx.NET, which starts at the very beginning but covers some advanced topics as well.
* [https://gist.github.com/staltz/868e7e9bc2a7b8c1f754](https://gist.github.com/staltz/868e7e9bc2a7b8c1f754) - Another introduction to Reactive Programming by the creator of [Cycle.js](https://cycle.js.org/).
* [http://reactivex.io](http://reactivex.io) - Probably the most comprehensive site for Reactive Programming. Contains a short introduction to Reactive Programming and documentation for many Rx operators in different languages.
* [http://rxmarbles.com](http://rxmarbles.com) - Interactive marble diagrams for some RxJS operators.

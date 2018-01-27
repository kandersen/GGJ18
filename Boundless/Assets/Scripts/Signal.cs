using System;
using System.Collections.Generic;

public class Signal
{
    private readonly List<Action> _listeners = new List<Action>();

    public void AddListener(Action listener)
    {
        _listeners.Add(listener);
    }

    public void Dispatch()
    {
        foreach (var listener in _listeners)
        {
            listener.Invoke();   
        }
    }

    public void RemoveListener(Action listener)
    {
        _listeners.Remove(listener);
    }
}

public class Signal<T>
{
    private readonly List<Action<T>> _listeners = new List<Action<T>>();

    public void AddListener(Action<T> listener)
    {
        _listeners.Add(listener);
    }

    public void Dispatch(T value)
    {
        foreach (var listener in _listeners)
        {
            listener.Invoke(value);   
        }
    }    
}
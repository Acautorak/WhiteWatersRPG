using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Notifier : Singleton<Notifier>
{
    private readonly Dictionary<Type, List<object>> subscribersByType = new();

    public void Subscribe<T>(Action<T> subscriber)
    {
        if (subscriber == null) return;

        var messageType = typeof(T);
        if (!subscribersByType.ContainsKey(messageType))
        {
            subscribersByType[messageType] = new List<object>();
        }

        var subscribers = subscribersByType[messageType];
        if (!subscribers.Contains(subscriber))
        {
            subscribersByType[messageType].Add(subscriber);
        }
    }

    public void Unsubscribe<T>(Action<T> subscriber)
    {
        if (subscriber == null) return;

        var messageType = typeof(T);
        if (!subscribersByType.ContainsKey(messageType))
        {
            return;
        }

        var subscribers = subscribersByType[messageType];
        if (subscribers.Contains(subscriber))
        {
            subscribersByType[messageType].Remove(subscriber);
        }
    }

    public void Notify<T>(T message)
    {
        if (message == null) return;

        var messageType = typeof(T);
        if (!subscribersByType.ContainsKey(messageType))
        {
            return;
        }

        // Use a copy of the list so nothing weird happens if the original list changes during iteration.
        var subscribersCopy = subscribersByType[messageType].ToList();
        foreach (Action<T> subscriber in subscribersCopy)
        {
            try
            {
                subscriber?.Invoke(message);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
    }
}
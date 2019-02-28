using System;
using System.Collections.Generic;

namespace Core
{
    public static class EventsController
    {
        private static Dictionary<EventsType, Delegate> events = new Dictionary<EventsType, Delegate>();

        public static void AddListener(EventsType eventName, Action callback)
        {
            SetupEvent(eventName);
            events[eventName] = (Action)events[eventName] + callback;
        }

        public static void AddListener<T>(EventsType eventName, Action<T> callback)
        {
            SetupEvent(eventName);
            events[eventName] = (Action<T>)events[eventName] + callback;
        }

        public static void AddListener<T, U>(EventsType eventName, Action<T, U> callback)
        {
            SetupEvent(eventName);
            events[eventName] = (Action<T, U>)events[eventName] + callback;
        }

        public static void RemoveListener(EventsType eventName, Action callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName] = (Action)events[eventName] - callback;
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T>(EventsType eventName, Action<T> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName] = (Action<T>)events[eventName] - callback;
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T, U>(EventsType eventName, Action<T, U> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName] = (Action<T, U>)events[eventName] - callback;
                ListenerRemoved(eventName);
            }
        }

        public static void Broadcast(EventsType eventName)
        {
            if (CanCall(eventName))
                ((Action)events[eventName])();
        }

        public static void Broadcast<T>(EventsType eventName, T param)
        {
            if (CanCall(eventName))
                ((Action<T>)events[eventName])(param);
        }

        public static void Broadcast<T, U>(EventsType eventName, T param, U param2)
        {
            if (CanCall(eventName))
                ((Action<T, U>)events[eventName])(param, param2);
        }

        private static bool CanCall(EventsType eventName)
        {
            return events.ContainsKey(eventName) && events[eventName] != null;
        }

        private static void ListenerRemoved(EventsType eventName)
        {
            if (events.ContainsKey(eventName) && events[eventName] == null)
                events.Remove(eventName);
        }

        private static void SetupEvent(EventsType eventName)
        {
            if (!events.ContainsKey(eventName))
                events.Add(eventName, null);
        }
    }
}
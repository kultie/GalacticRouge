using System;
using System.Collections.Generic;

namespace Core.EventDispatcher
{
    public delegate void Caller(Dictionary<string, object> args);
    public class EventDispatcher
    {
        private static Dictionary<string, Caller> listeners = new Dictionary<string, Caller>();

        public static void Subscribe(string key, Caller evt)
        {
            if (!listeners.ContainsKey(key))
            {
                listeners[key] = null;
            }
            listeners[key] += evt;
        }

        public static void Dispatch(string key, Dictionary<string, object> args)
        {
            if (listeners.ContainsKey(key))
            {
                listeners[key]?.Invoke(args);
            }
        }

        public static void UnSubscribe(string key, Caller evt)
        {
            if (listeners.ContainsKey(key))
            {
                listeners[key] -= evt;
            }
        }

        public static void Clear()
        {
            listeners.Clear();
        }
    }
}
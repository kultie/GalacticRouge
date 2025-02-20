using System;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.TimerSystem
{
    public class TickTimer
    {
        Dictionary<string, TickTimerElement> dicTimer;

        public bool isRunning
        {
            get
            {
                return dicTimer.Count > 0;
            }
        }
        public TickTimer()
        {
            dicTimer = new Dictionary<string, TickTimerElement>();
        }

        public void After(int delay, Action action, bool repeatable = false, string tag = "")
        {
            TickTimerElement timer = new TickTimerElement(delay, action, repeatable);
            if (string.IsNullOrEmpty(tag)) tag = Guid.NewGuid().ToString();
            if (dicTimer.ContainsKey(tag))
            {
                dicTimer[tag] = timer;
            }
            else
            {
                dicTimer.Add(tag, timer);
            }
        }

        public void After(int delay, Action action, int repeatTime, string tag = "")
        {
            TickTimerElement timer = new TickTimerElement(delay, action, repeatTime);
            if (string.IsNullOrEmpty(tag)) tag = DateTime.Now.ToString();
            if (dicTimer.ContainsKey(tag))
            {
                dicTimer[tag] = timer;
            }
            else
            {
                dicTimer.Add(tag, timer);
            }
        }

        public void Update()
        {
            var list = new Dictionary<string, TickTimerElement>(dicTimer);
            foreach (KeyValuePair<string, TickTimerElement> val in list)
            {
                TickTimerElement timer = val.Value;
                timer.currentTick = timer.currentTick + 1;
                if (timer.currentTick > timer.delay)
                {
                    if (timer.action != null)
                    {
                        timer.action?.Invoke();
                    }
                    if (timer.repeatable)
                    {
                        if (timer.repeatTime > 0)
                        {
                            timer.repeatTime = timer.repeatTime - 1;
                            if (timer.repeatTime > 0)
                            {
                                timer.currentTick = 0;
                            }
                            else
                            {
                                timer.repeatable = false;
                                dicTimer.Remove(val.Key);
                            }
                        }
                        else
                        {
                            timer.currentTick = 0;
                        }
                    }
                    else
                    {
                        dicTimer.Remove(val.Key);
                    }
                }
            }
        }

        public void RemoveTag(string tag)
        {
            dicTimer.Remove(tag);
        }

        public void Clear()
        {
            dicTimer.Clear();
        }
    }

    class TickTimerElement
    {
        public int delay;
        public Action action;
        public bool repeatable;
        public int repeatTime;
        public int currentTick;

        public TickTimerElement(int _delay, Action _action, bool _repeatable)
        {
            delay = _delay;
            action = _action;
            repeatable = _repeatable;
            currentTick = 0;
        }

        public TickTimerElement(int _delay, Action _action, int _repeatTime)
        {
            delay = _delay;
            action = _action;
            repeatTime = _repeatTime;
            currentTick = 0;
            if (repeatTime > 0)
            {
                repeatable = true;
            }
        }
    }
}
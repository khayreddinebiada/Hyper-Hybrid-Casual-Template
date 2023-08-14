using System.Collections.Generic;
using System;

namespace HCEngine.Events
{
    public class Event<T> : IEvent<T> where T : class
    {
        protected List<T> Subscribes = new List<T>();

        public virtual void Subscribe(T eventObject)
        {
            if (eventObject == null) throw new ArgumentNullException();

            Subscribes.Add(eventObject);
        }

        public virtual void Unsubscribe(T eventObject)
        {
            if (eventObject == null) throw new ArgumentNullException();

            Subscribes.Remove(eventObject);
        }

        public void UnsubscribeAll()
        {
            Subscribes.Clear();
        }

        public void CleanNulls()
        {
            Subscribes.RemoveAll(item => item == null || item.Equals(null));
        }

        public virtual void Invoke(Action<T> invoke)
        {
            ICollection<T> subs = new List<T>(Subscribes);
            foreach (var item in subs)
            {
                if (item != null && !item.Equals(null))
                    invoke.Invoke(item);
            }
        }
    }
}
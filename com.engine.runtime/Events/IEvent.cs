using System;

namespace HCEngine.Events
{
    public interface IEvent<T> : IEventSubscribe<T>
    {
        void UnsubscribeAll();

        void CleanNulls();

        void Invoke(Action<T> invoke);
    }
}
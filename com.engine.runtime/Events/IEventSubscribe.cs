namespace HCEngine.Events
{
    public interface IEventSubscribe<T>
    {
        void Subscribe(T handler);

        void Unsubscribe(T handler);
    }
}
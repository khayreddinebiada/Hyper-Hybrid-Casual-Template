namespace HCEngine.Data
{
    public interface IValueField<T>
    {
        public T Value { get; }

        public bool HasValue { get; }

        public string FileName { get; }

        public void Save();

        public void DeleteKey();
    }
}
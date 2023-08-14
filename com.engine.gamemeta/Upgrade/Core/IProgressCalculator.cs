namespace HCEngine.Upgrade
{
    public interface IProgressCalculator
    {
        public float InitValue { get; set; }
        public float CurrentResult { get; }
        public void Reset();
        public void InsertValue(float value);
    }
}

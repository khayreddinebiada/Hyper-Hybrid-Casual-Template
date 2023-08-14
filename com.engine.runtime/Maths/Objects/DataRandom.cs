namespace HCEngine.Random
{
    public struct DataRandom : IRandomRarity
    {
        public int Value { get; }
        public float RarityValue { get; }

        public DataRandom(int value, float rarityValue)
        {
            Value = value;
            RarityValue = rarityValue;
        }
    }
}
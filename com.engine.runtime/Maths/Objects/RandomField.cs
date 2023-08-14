
namespace HCEngine.Random
{
    public struct RandomField : IRandomField
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public RandomField(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int GetRandomValue()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
}
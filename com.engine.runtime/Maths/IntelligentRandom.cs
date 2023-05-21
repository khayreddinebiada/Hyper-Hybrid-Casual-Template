using System.Collections.Generic;

namespace Engine.Random
{
    public struct DataRandom
    {
        public int Value { get; }
        public float Probability { get; }

        public DataRandom(int value, float probability)
        {
            Value = value;
            Probability = probability;
        }
    }

    public struct RandomFieldInfo
    {
        public int min { get; private set; }
        public int max { get; private set; }

        public RandomFieldInfo(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int GetRandomValue()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

    public static class IntelligentRandom
    {
        /// <summary>
        /// You can make random by percent of all possibilities.
        /// </summary>
        public static int CustomRandom(DataRandom[] dataRandoms)
        {
            float randomValue = UnityEngine.Random.Range(0, 100);
            float lastRange = 0;
            for (int i = 0; i < dataRandoms.Length; i++)
            {
                if (lastRange <= randomValue && randomValue < lastRange + dataRandoms[i].Probability)
                    return dataRandoms[i].Value;
                else
                    lastRange += dataRandoms[i].Probability;
            }

            if (lastRange < 99f)
                throw new System.ArgumentException("The data random is not right, please give total 100 right percents");
            else
                return 0;
        }

        public static T[] GetRandomsNotRepeated<T>(T[] values, int numbersNeeds)
        {
            if (values == null) throw new System.ArgumentNullException();
            if ((uint)values.Length < (uint)numbersNeeds) throw new System.ArgumentOutOfRangeException();

            List<T> rands = new List<T>(values);
            T[] returns = new T[numbersNeeds];
            for (int i = 0; i < numbersNeeds; i++)
            {
                returns[i] = rands[UnityEngine.Random.Range(0, rands.Count)];
                rands.Remove(returns[i]);
            }

            return returns;
        }

        public static int GetRandomWithField(RandomFieldInfo[] fieldInfos)
        {
            return fieldInfos[UnityEngine.Random.Range(0, fieldInfos.Length)].GetRandomValue();
        }

        public static float RandomParabola(float value)
        {
            return (UnityEngine.Random.value - 0.5f) * 2 * value;
        }
    }
}

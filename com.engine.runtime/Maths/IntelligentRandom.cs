using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace HCEngine.Random
{
    public static class IntelligentRandom
    {
        public static bool GenerateRandomRarity<T>(this IEnumerable<T> values, out T result) where T : IRandomRarity
        {
            if (values == null) throw new ArgumentNullException();


            float totalRarities = TotalRarities(values);

            float random = UnityEngine.Random.Range(0, totalRarities);

            float countRarity = 0;

            foreach (var rarity in values)
            {
                /// Add index.
                if (countRarity <= random && random < countRarity + rarity.RarityValue)
                {
                    result = rarity;
                    return true;
                }
                else
                    countRarity += rarity.RarityValue;
            }

            throw new ArithmeticException("The rarities values are has an invalid values!...");
        }

        public static T[] GenerateRandomRarities<T>(this IEnumerable<T> values, int numbersNeeds, bool isRepeatitive = true) where T : IRandomRarity
        {
            if (values == null) throw new ArgumentNullException();

            List<T> collection = values.ToList();

            if (!isRepeatitive && (uint)collection.Count < (uint)numbersNeeds)
                throw new ArgumentOutOfRangeException("The numbersNeeds value is less then the values.Count!...");

            T[] result = new T[numbersNeeds];

            int filledIndex = 0;

            while(filledIndex < numbersNeeds)
            {
                if (collection.GenerateRandomRarity(out T output))
                {
                    result[filledIndex] = output;
                    filledIndex++;

                    if (!isRepeatitive)
                        collection.Remove(output);
                }
                else
                    throw new ArithmeticException("The rarities values are has an invalid values!...");
            }

            return result;
        }

        public static float TotalRarities<T>(this IEnumerable<T> values) where T : IRandomRarity
        {
            float sum = 0;

            foreach (var item in values)
            {
                Contract.Assert(0 < item.RarityValue, "The rarity value is should be bigger then zero!...");
                sum += item.RarityValue;
            }

            return sum;
        }

        [Obsolete]
        public static int CustomRandom(DataRandom[] dataRandoms)
        {
            float randomValue = UnityEngine.Random.Range(0, 100);
            float lastRange = 0;
            for (int i = 0; i < dataRandoms.Length; i++)
            {
                if (lastRange <= randomValue && randomValue < lastRange + dataRandoms[i].RarityValue)
                    return dataRandoms[i].Value;
                else
                    lastRange += dataRandoms[i].RarityValue;
            }

            if (lastRange < 99f)
                throw new System.ArgumentException("The data random is not right, please give total 100 right percents");
            else
                return 0;
        }

        public static T[] GetNotRepeatedValues<T>(this T[] values, int numbersNeeds)
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

        public static int GetRandomWithField<T>(this T[] fieldInfos) where T : IRandomField
        {
            T result = fieldInfos[UnityEngine.Random.Range(0, fieldInfos.Length)];
            return UnityEngine.Random.Range(result.Min, result.Max);
        }

        public static float RandomParabola(float value)
        {
            return (UnityEngine.Random.value - 0.5f) * 2 * value;
        }
    }
}

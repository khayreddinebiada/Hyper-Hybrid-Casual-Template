using HCEngine.Attributes;
using HCEngine.Data;
using UnityEngine;

namespace HCEngine.Currency
{
    [Asset]
    [CreateAssetMenu(fileName = "New Currency Info", menuName = "Add/Currency Info", order = 3)]
    public class CurrencyInfo : ScriptableObject
    {
        public ValueField<int> CurrentValue;

        public int InitValue = 0;
    }
}
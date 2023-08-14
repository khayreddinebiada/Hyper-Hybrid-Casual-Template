using UnityEngine;
using HCEngine.Attributes;
using HCEngine.Data;

namespace HCEngine.Store
{
    [Asset]
    [CreateAssetMenu(fileName = "New Store", menuName = "Add/Store/Add Store", order = 1)]
    public class StoreInfo : ScriptableObject
    {
        [Header("Settings")]
        public ProductConfig[] ProductInfos;

        [Header("Data")]
        public ValueField<int>[] ProductFields;
    }
}
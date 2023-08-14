using UnityEngine;

namespace HCEngine.Store
{
    [CreateAssetMenu(fileName = "New ProductInfo", menuName = "Add/Store/New ProductInfo", order = 1)]
    public partial class ProductConfig : ScriptableObject
    {
        public string Name;
        public string Cagegory;
        public ProductSettings Settings;
    }
}
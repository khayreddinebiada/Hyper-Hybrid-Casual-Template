using HCEngine.Balance;
using HCEngine.Data;
using HCEngine.DI;
using UnityEngine;

namespace HCEngine.Upgrade
{
    [CreateAssetMenu(fileName = "New Upgradable Info", menuName = "Add/Upgrades/Upgradable Info", order = 10)]
    public class UpgradableInfo : ScriptableObject
    {
        [Header("Save Parameters")]
        [Identificator(((int)Systems.Upgradeable))]public int Id;
        public ValueField<int> Level;

        [Header("Info Parameters")]
        public UpgradableSettings[] UpgradeInfos;
        public BalanceInfo[] BalanceInfos;

        [Header("Reset Parameters")]
        [Min(0)] public int InitLevel = 0;
    }
}

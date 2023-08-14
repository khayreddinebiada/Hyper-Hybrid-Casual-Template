using HCEngine.Balance;
using HCEngine.Data;
using UnityEngine;

namespace HCEngine.Upgrade
{
    [CreateAssetMenu(fileName = "New Upgrade Card Info", menuName = "Add/Upgrades/Upgrade Card Info", order = 10)]
    public class UpgradableCardInfo : UpgradableInfo
    {
        [Header("Save Parameters")]
        public ValueField<int> CardsAvailable;

        [Header("Info Parameters")]
        public BalanceInfo BalanceCardsRequired;

        public int InitCards = 0;
    }
}

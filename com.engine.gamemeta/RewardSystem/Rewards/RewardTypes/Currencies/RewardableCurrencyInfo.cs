using UnityEngine;

namespace HCEngine.RewardSystem
{
    [CreateAssetMenu(fileName = "New Rewardables Currencies Info", menuName = "Add/Reward/Rewardables Currencies Info", order = 1)]
    public class RewardableCurrencyInfo : ScriptableObject
    {
        [Identificator((int)Systems.Rewardable)] public int RewardableId;
        [Identificator((int)Systems.Currency)] public int CurrencyId;
    }
}
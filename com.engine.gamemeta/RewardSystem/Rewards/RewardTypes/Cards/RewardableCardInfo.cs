using UnityEngine;

namespace HCEngine.RewardSystem
{
    [CreateAssetMenu(fileName = "New Rewardables Cards Info", menuName = "Add/Reward/Rewardables Cards Info", order = 1)]
    public class RewardableCardInfo : ScriptableObject
    {
        [Identificator((int)Systems.Rewardable)] public int RewardableId;
        [Identificator((int)Systems.Upgradeable)] public int UpgradeableId;
    }
}
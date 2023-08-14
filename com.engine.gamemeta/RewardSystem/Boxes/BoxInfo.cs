using HCEngine.RewardSystem.Generic;
using UnityEngine;

namespace HCEngine.RewardSystem
{
    [CreateAssetMenu(fileName = "New Box", menuName = "Add/Reward/New Box", order = 1)]
    public class BoxInfo : ScriptableObject
    {
        [Identificator((int)Systems.Box)] public int BoxId;
        [Min(1)] public int RewardsCount;
        public bool IsRepeatable;
        public RewardRarityConfig[] Configs;
    }
}
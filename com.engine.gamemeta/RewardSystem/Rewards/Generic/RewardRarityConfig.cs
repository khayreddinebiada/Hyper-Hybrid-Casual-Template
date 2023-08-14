using HCEngine.Random;
using UnityEngine;

namespace HCEngine.RewardSystem.Generic
{
    [System.Serializable]
    public struct RewardRarityConfig : IRandomRarity
    {
        [Identificator((int)Systems.Rewardable)] public int RewardableId;

        public Vector2Int CountRange;
        public float Rarity;
        public float RarityValue => Rarity;
    }
}
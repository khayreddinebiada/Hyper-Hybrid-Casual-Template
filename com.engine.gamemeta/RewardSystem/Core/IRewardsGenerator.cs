using HCEngine.RewardSystem.Generic;
using System.Collections.Generic;

namespace HCEngine.RewardSystem
{
    public interface IRewardsGenerator
    {
        IEnumerator<PairCountRewardable> Generate(RewardRarityConfig[] configs, int rewardsCount, bool isRepeatable);
    }
}
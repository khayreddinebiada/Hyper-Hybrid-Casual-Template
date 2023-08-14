using HCEngine.DI;
using HCEngine.Random;
using HCEngine.RewardSystem.Generic;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace HCEngine.RewardSystem
{
    public class RewardsGenerator : IRewardsGenerator
    {
        IEnumerator<PairCountRewardable> IRewardsGenerator.Generate(RewardRarityConfig[] configs, int rewardsCount, bool isRepeatable)
        {
            RewardRarityConfig[] randomsConfigs = configs.GenerateRandomRarities(rewardsCount, isRepeatable);

            for (int i = 0; i < rewardsCount; i++)
            {
                RewardRarityConfig config = randomsConfigs[i];

                yield return new PairCountRewardable(
                    UnityEngine.Random.Range(config.CountRange.x, config.CountRange.y),
                    FindRewardable(config.RewardableId));
            }
        }

        private IRewardable FindRewardable(int id)
        {
            IRewardable rewardable = DIContainer.GetValueOfId<IRewardable>((int)id);
            Contract.Assert(rewardable != null, $"The rewardable of id {id} not found!");

            return rewardable;
        }
    }
}
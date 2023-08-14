using System.Collections.Generic;

namespace HCEngine.RewardSystem
{
    public interface IBox : IEnumerable<PairCountRewardable>
    {
        int Id { get; }
        
        void GenerateRewards();

        void ClaimAll();
    }
}

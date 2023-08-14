using System;

namespace HCEngine.RewardSystem
{
    public struct PairCountRewardable
    {
        public int Count;
        public IRewardable Rewardable;

        public PairCountRewardable(int count, IRewardable rewardable)
        {
            Count = count;
            Rewardable = rewardable;
        }

        public void Claim()
        {
            Rewardable.Claim(Count);
        }

        public override bool Equals(object obj)
        {
            if (obj is IRewardable)
                return Rewardable.Equals((IRewardable)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rewardable);
        }
    }
}
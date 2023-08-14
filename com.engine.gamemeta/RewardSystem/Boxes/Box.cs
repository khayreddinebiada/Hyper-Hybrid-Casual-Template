using System.Linq;
using System.Collections;
using System.Collections.Generic;
using HCEngine.RewardSystem.Generic;
using HCEngine.DI;
using System.Diagnostics.Contracts;
using System;

namespace HCEngine.RewardSystem
{
    public class Box : IBox, IDependency
    {
        private int _identificator;
        private bool _isRepeatable;
        private int _rewardsCount;
        private RewardRarityConfig[] _configs;

        private IRewardsGenerator _generator;
        private PairCountRewardable[] _rewards;

        public int Id => _identificator;
        public int RewardsCount => _rewardsCount;

        public Box(int identificator, IRewardsGenerator generator, RewardRarityConfig[] configs, int rewardsCount, bool isRepeatable)
        {
            Contract.Requires(0 < rewardsCount, "The rewards count should be more them zero!...");

            _identificator = identificator;
            _configs = configs;
            _generator = generator;
            _isRepeatable = isRepeatable;
            _rewardsCount = rewardsCount;
        }

        public Box(BoxInfo info, IRewardsGenerator generator) :
            this(info.BoxId, generator, info.Configs, info.RewardsCount, info.IsRepeatable) { }

        public Box(BoxInfo info) :
            this(info.BoxId, new RewardsGenerator(), info.Configs, info.RewardsCount, info.IsRepeatable) { }

        public void ThrowExceptionIfNotGenerated()
        {
            if (_rewards == null)
                throw new InvalidOperationException("Please generate Rewards before call this function!...");
        }

        public void Inject()
        {
            DIContainer.Register<IBox>(_identificator, this);
        }

        public void GenerateRewards()
        {
            FillPairCountRewards();
        }

        private void FillPairCountRewards()
        {
            _rewards = new PairCountRewardable[_rewardsCount];
            int index = 0;
            using (IEnumerator<PairCountRewardable> en = _generator.Generate(_configs, _rewardsCount, _isRepeatable))
            {
                while (en.MoveNext() && (uint)index < (uint)_rewards.Length)
                {
                    _rewards[index] = en.Current;
                    index++;
                }
            }
        }

        public void ClaimAll()
        {
            ThrowExceptionIfNotGenerated();

            for (int i = 0; i < _rewards.Length; i++)
            {
                _rewards[i].Claim();
            }
        }

        public IEnumerator<PairCountRewardable> GetEnumerator()
        {
            ThrowExceptionIfNotGenerated();

            return _rewards.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            ThrowExceptionIfNotGenerated();

            return _rewards.AsEnumerable().GetEnumerator();
        }
    }
}

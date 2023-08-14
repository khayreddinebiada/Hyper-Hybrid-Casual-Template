using HCEngine.DI;
using UnityEngine;

namespace HCEngine.RewardSystem
{
    public abstract class Rewardable : IRewardable, IDependency
    {
        [SerializeField, Identificator((int)Systems.Rewardable)] protected int _rewardId;

        public Rewardable(int identificator)
        {
            _rewardId = identificator;
        }

        public void Inject()
        {
            DIContainer.Register<IRewardable>(_rewardId, this);
        }

        public abstract void Claim(int count);
    }
}
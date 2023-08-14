using HCEngine.Upgrade;
using HCEngine.DI;
using System.Diagnostics.Contracts;

namespace HCEngine.RewardSystem
{
    public class CardReward : Rewardable, IAwake
    {
        private int _cardIdentificator;
        private IUpgradeableCard _upgradeable;

        public CardReward(int rewardableIdentificator, int cardIdentificator) : base(rewardableIdentificator)
        {
            _cardIdentificator = cardIdentificator;
        }

        public void Awake()
        {
            InitializeUpgradeable();
        }

        public void InitializeUpgradeable()
        {
            IUpgradeable upgradeable = DIContainer.GetValueOfId<IUpgradeable>(_cardIdentificator);
            Contract.Assert(upgradeable != null, $"The card of id {_cardIdentificator} not found!");
            _upgradeable = upgradeable as IUpgradeableCard;
            Contract.Assert(_upgradeable != null, $"The upgradeable of id {_cardIdentificator} is non casting to IUpgradeableCard!");
        }

        public override void Claim(int count)
        {
            _upgradeable.AddCard(count);
        }

    }
}
using System.Diagnostics.Contracts;
using HCEngine.Balance;
using HCEngine.Data;
using HCEngine.Linq;
using HCEngine.DI;

namespace HCEngine.Upgrade
{
    [System.Serializable]
    public class UpgradeableCard : Upgradeable, IUpgradeableCard, IDependency
    {
        private IBalanceInfo _balanceCardsRequired;
        private ValueField<int> _cardsAvailable;

        public int CardsAvailable => _cardsAvailable.Value;
        public int CardsRequired => (int)_balanceCardsRequired.GetValue(Level);
        public bool IsAbleUpgrade => CardsRequired <= CardsAvailable;

        public UpgradeableCard(UpgradableInfo info) : base(info)
        {
            Contract.Assert(!info.IsNull());
            Contract.Assert(info is UpgradableCardInfo);
            Contract.EndContractBlock();

            InitializeInfo();
        }

        private void InitializeInfo()
        {
            Contract.Requires(Info is UpgradableCardInfo, $"The {nameof(UpgradableInfo)} is not type of {nameof(UpgradableCardInfo)}!...");

            UpgradableCardInfo info = (UpgradableCardInfo)Info;
            _balanceCardsRequired = info.BalanceCardsRequired;
            _cardsAvailable = info.CardsAvailable;
        }

        public void AddCard(int count)
        {
            _cardsAvailable.Value += count;
        }

        public override void MakeUpgrade()
        {
            if (CardsRequired <= CardsAvailable)
            {
                _cardsAvailable.Value -= CardsRequired;

                base.MakeUpgrade();
            }
        }
    }
}
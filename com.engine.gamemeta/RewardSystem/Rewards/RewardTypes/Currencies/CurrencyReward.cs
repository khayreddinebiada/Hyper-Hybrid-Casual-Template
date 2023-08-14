using HCEngine.Currency;
using HCEngine.DI;
using System.Diagnostics.Contracts;

namespace HCEngine.RewardSystem
{
    public class CurrencyReward : Rewardable, IAwake
    {
        private int _idCurrency;
        private ICurrency _currency;

        public CurrencyReward(int identificator, int idCurrency) : base(identificator)
        {
            _idCurrency = idCurrency;
        }

        public void Awake()
        {
            InitializeCurrency();
        }

        public void InitializeCurrency()
        {
            _currency = DIContainer.GetValueOfId<ICurrency>(_idCurrency);
            Contract.Assert(_currency != null, $"The currency of id {_idCurrency} is not found!");
        }

        public override void Claim(int count)
        {
            _currency.AddCurrency(count);
        }
    }
}
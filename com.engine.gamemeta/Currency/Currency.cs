using HCEngine.DI;
using HCEngine.Data;
using UnityEngine;
using HCEngine.Events;

namespace HCEngine.Currency
{
    [System.Serializable]
    public class Currency : IAwake, ICurrency, IDependency
    {
        private Event<ICurrencyUpdated> _onCurrencyUpdated = new Event<ICurrencyUpdated>();

        [SerializeField, Identificator((int)Systems.Currency)] private int _currencyId;
        [SerializeField] private CurrencyInfo _currencyInfo;

        private ValueField<int> _currentValue;

        public int TotalCoins => _currentValue.Value;
        public IEventSubscribe<ICurrencyUpdated> OnCurrencyUpdated => _onCurrencyUpdated;

        public void Inject()
        {
            DIContainer.Register<ICurrency>(_currencyId, this);
        }

        public void Awake()
        {
            _currentValue = _currencyInfo.CurrentValue;
        }

        public bool AddCurrency(int amount)
        {
            if (amount <= 0) return false;

            /// Update data.
            _currentValue.Value += amount;

            /// Fill data delegate.
            ParametersUpdate dData = new ParametersUpdate();
            dData.total = _currentValue.Value;
            dData.amount = amount;

            // Execute delegate.
            _onCurrencyUpdated.Invoke((updated) => updated.OnCurrencyUpdated(dData));
            return true;
        }

        public bool RemoveCurrency(int amount)
        {
            if (amount <= 0 || _currentValue.Value < amount) return false;

            /// Update data.
            _currentValue.Value = Mathf.Clamp(_currentValue.Value - amount, 0, _currentValue.Value);

            /// Fill data delegate.
            ParametersUpdate dData = new ParametersUpdate();
            dData.total = _currentValue.Value;
            dData.amount = -amount;

            // Execute delegate.
            _onCurrencyUpdated.Invoke((updated) => updated.OnCurrencyUpdated(dData));
            return true;
        }
    }
}

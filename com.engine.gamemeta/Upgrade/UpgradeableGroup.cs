using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Collections;
using HCEngine.Balance;
using HCEngine.Events;
using System.Linq;

namespace HCEngine.Upgrade
{
    public class UpgradeableGroup : IUpgradeableGroup, IUpgraded
    {
        public int Count
            => _balances.Count;

        public float this[string key]
            => _balances[key].CurrentResult;

        public IEnumerable<string> Names
            => _balances.Keys;

        public IEnumerable<float> Values
        {
            get
            {
                foreach (IProgressCalculator calculator in _balances.Values)
                    yield return calculator.CurrentResult;
            }
        }

        public IEnumerable<IUpgradeable> Upgradeables
            => _upgradeables;

        public IEventSubscribe<IUpdatedValues> OnUpdatedValues
            => _events;

        private Event<IUpdatedValues> _events = new Event<IUpdatedValues>();
        private SortedList<string, IProgressCalculator> _balances;
        private IUpgradeable[] _upgradeables;

        public UpgradeableGroup(IEnumerable<IUpgradeable> upgradeables)
        {
            _balances = new SortedList<string, IProgressCalculator>();

            CreateGroup(upgradeables);

            RecalculateBalances();
        }

        public UpgradeableGroup(IEnumerable<IUpgradeable> upgradeables, IDictionary<string, IProgressCalculator> pairs)
        {
            Contract.Assert(pairs != null, "The array that you are trying to create has a null value!...");

            _balances = new SortedList<string, IProgressCalculator>(pairs);

            CreateGroup(upgradeables);

            RecalculateBalances();
        }

        private void CreateGroup(IEnumerable<IUpgradeable> upgradeables)
        {
            Contract.Assert(upgradeables != null, $"The {nameof(upgradeables)} that you are trying to add has a null value!...");

            _upgradeables = upgradeables.ToArray();

            SubscribeUpgrades(_upgradeables);
        }
        
        private void SubscribeUpgrades(IUpgradeable[] upgradeables)
        {
            foreach (var item in upgradeables)
            {
                if (item == null || item.Equals(null))
                    continue;
                item.OnUpgraded.Subscribe(this);
            }
        }

        public void Add(string balanceName, IProgressCalculator calculator)
        {
            calculator = RecalculateBalance(balanceName, calculator);

            _balances.Add(balanceName, calculator);

            InvokeOnUpdate();
        }

        public bool TryAdd(string balanceName, IProgressCalculator calculator)
        {
            return _balances.TryAdd(balanceName, calculator);
        }

        public bool ContainsKey(string balanceName)
        {
            return _balances.ContainsKey(balanceName);
        }

        public bool Remove(string balanceName)
        {
            return _balances.Remove(balanceName);
        }

        public bool TryGetValue(string balanceName, out float value)
        {
            value = 0;
            bool isFaund = _balances.TryGetValue(balanceName, out IProgressCalculator calculator);
            if (isFaund)
                value = calculator.CurrentResult;

            return isFaund;
        }

        public void Clear()
        {
            _balances.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
        {
            foreach (KeyValuePair<string, IProgressCalculator> pair in _balances)
                yield return new KeyValuePair<string, float>(pair.Key, pair.Value.CurrentResult);
        }

        public bool TryGetBalanceCalculator(string balanceName, out IProgressCalculator calculator)
        {
            return _balances.TryGetValue(balanceName, out calculator);
        }

        public IProgressCalculator GetBalanceCalculator(string balanceName)
        {
            return _balances[balanceName];
        }

        public void RecalculateBalances()
        {
            foreach (KeyValuePair<string, IProgressCalculator> pair in _balances)
            {
                pair.Value.Reset();
                foreach (float value in GetBalancesValuesFromUpgrades(pair.Key, _upgradeables))
                    pair.Value.InsertValue(value);
            }

            InvokeOnUpdate();
        }

        private IProgressCalculator RecalculateBalance(string balanceName, IProgressCalculator calculator)
        {
            calculator.Reset();
            foreach (float value in GetBalancesValuesFromUpgrades(balanceName, _upgradeables))
            {
                calculator.InsertValue(value);
            }
            return calculator;
        }

        private IEnumerable<float> GetBalancesValuesFromUpgrades(string balanceName, IUpgradeable[] upgradeables)
        {
            foreach (IUpgradeable upgradeable in upgradeables)
            {
                if (upgradeable == null || upgradeable.Equals(null))
                    continue;

                if (upgradeable.TryGetBalanceInfo(balanceName, out IBalanceInfo value))
                    yield return value.GetValue(upgradeable.Level);
            }
        }

        private void InvokeOnUpdate()
        {
            _events.Invoke((values) => values.OnUpdatedValues());
        }

        public void OnUpgrade(int level)
        {
            RecalculateBalances();
        }

        public void MondifyCalculator(string balanceName, IProgressCalculator newCalculator)
        {
            _balances[balanceName] = newCalculator;
        }
    }
}

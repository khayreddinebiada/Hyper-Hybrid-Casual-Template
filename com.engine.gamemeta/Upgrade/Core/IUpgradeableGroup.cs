using HCEngine.Events;
using System.Collections.Generic;

namespace HCEngine.Upgrade
{
    public interface IUpgradeableGroup : IEnumerable<KeyValuePair<string, float>>
    {
        int Count { get; }
        float this[string balanceName] { get; }

        IEnumerable<string> Names { get; }
        IEnumerable<float> Values { get; }

        IEnumerable<IUpgradeable> Upgradeables { get; }
        IEventSubscribe<IUpdatedValues> OnUpdatedValues { get; }


        void Add(string balanceName, IProgressCalculator calculator);
        void MondifyCalculator(string balanceName, IProgressCalculator newCalculator);
        bool Remove(string balanceName);
        bool ContainsKey(string balanceName);


        bool TryAdd(string balanceName, IProgressCalculator calculator);
        bool TryGetValue(string balanceName, out float value);
        bool TryGetBalanceCalculator(string balanceName, out IProgressCalculator calculator);

        IProgressCalculator GetBalanceCalculator(string balanceName);


        void Clear();

        void RecalculateBalances();
    }
}
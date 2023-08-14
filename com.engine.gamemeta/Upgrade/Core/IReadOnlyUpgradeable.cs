using HCEngine.Balance;
using HCEngine.Events;
using System.Collections.Generic;

namespace HCEngine.Upgrade
{
    public interface IReadOnlyUpgradeable : IEnumerable<IBalanceInfo>
    {
        IEventSubscribe<IUpgraded> OnUpgraded { get; }

        int Level { get; }
        int CountSettings { get; }
        int CountBalances { get; }

        public IBalanceInfo this[string name] { get; }
        bool TryGetBalanceInfo(string name, out IBalanceInfo value);

        UpgradableSettings GetCurrentUpgradeInfo();

        IEnumerator<UpgradableSettings> GetUpgradeInfos();
    }
}
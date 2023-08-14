using HCEngine.Balance;
using System.Collections.Generic;
using System.Linq;

namespace HCEngine.Upgrade
{
    public static class UpgradableExtension
    {
        public static void ResetUpgradeData(this UpgradableInfo upgradeData, string fileName)
        {
            upgradeData.Level = new Data.ValueField<int>("Level", fileName, upgradeData.InitLevel);
        }

        public static void UnlockUpgradeData(this UpgradableInfo upgradeData, string fileName)
        {
            upgradeData.Level = new Data.ValueField<int>("Level", fileName, int.MaxValue);
        }

        public static void ResetUpgradeCardData(this UpgradableCardInfo upgradeData, string fileName)
        {
            upgradeData.CardsAvailable = new Data.ValueField<int>("CardsAvailable", fileName, upgradeData.InitCards);
        }

        public static void UnlockUpgradeCardData(this UpgradableCardInfo upgradeData, string fileName)
        {
            upgradeData.CardsAvailable = new Data.ValueField<int>("CardsAvailable", fileName, int.MaxValue);
        }

        public static SortedDictionary<string, float> GetBalanceValues(this IUpgradeable upgradable, int level = -1)
        {
            SortedDictionary<string, float> result = new SortedDictionary<string, float>();
            int targetLevel = (level < 0) ? upgradable.Level : level;

            foreach (var item in upgradable)
            {
                IBalanceInfo balance = item;
                if (balance == null)
                    continue;

                result.Add(balance.Name, balance.GetValue(targetLevel));
            }

            return result;
        }

        public static SortedDictionary<string, IBalanceInfo> ToSortedDictionary(this IBalanceInfo[] infos)
        {
            SortedDictionary<string, IBalanceInfo> pairs = new SortedDictionary<string, IBalanceInfo>();

            foreach (BalanceInfo balance in infos)
            {
                pairs.Add(balance.Name, balance);
            }

            return pairs;
        }

        public static string[] FindUpgradeNamesBalances(this IUpgradeableGroup upgradeableGroup)
        {
            SortedSet<string> names = new SortedSet<string>();

            foreach (IUpgradeable upgradeable in upgradeableGroup.Upgradeables)
            {
                if (upgradeable == null || upgradeable.Equals(null))
                    continue;

                foreach (IBalanceInfo balance in upgradeable)
                {
                    if (!names.Contains(balance.Name))
                    {
                        names.Add(balance.Name);
                    }
                }
            }

            return names.ToArray();
        }

    }
}
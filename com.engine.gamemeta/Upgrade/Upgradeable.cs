using System.Collections.Generic;
using System.Collections;
using HCEngine.Balance;
using HCEngine.Events;
using HCEngine.Data;
using HCEngine.Linq;
using HCEngine.DI;
using System.Linq;
using UnityEngine;

namespace HCEngine.Upgrade
{
    [System.Serializable]
    public class Upgradeable : IUpgradeable, IDependency
    {
        public Event<IUpgraded> _onUpgraded = new Event<IUpgraded>();
        public IEventSubscribe<IUpgraded> OnUpgraded => _onUpgraded;

        private int _upgradeableId;
        private ValueField<int> _level;
        private UpgradableSettings[] _upgradableSettings;
        private SortedDictionary<string, IBalanceInfo> _balanceInfo;

        protected UpgradableInfo Info { get; private set; }

        public IBalanceInfo this[string name]
        {
            get
            {
                return _balanceInfo[name];
            }
        }

        public int Level => _level.Value;
        public int CountSettings => _upgradableSettings.Length;
        public int CountBalances => _balanceInfo.Count;


        public void Inject()
        {
            DIContainer.Register<IUpgradeable>(_upgradeableId, this);
        }

        public Upgradeable(UpgradableInfo info)
        {
            if (info.IsNull())
                throw new System.ArgumentNullException(nameof(info));

            Info = info;

            InitializeInfo();
        }

        private void InitializeInfo()
        {
            _level = Info.Level;
            _upgradeableId = Info.Id;
            _upgradableSettings = Info.UpgradeInfos;
            _balanceInfo = Info.BalanceInfos.ToSortedDictionary();
        }

        public UpgradableSettings GetUpgradeInfo(int index)
        {
            if (_upgradableSettings.Length <= (uint)index)
                throw new System.ArgumentOutOfRangeException("The index of UpgradeInfo");

            return _upgradableSettings[index];
        }

        public UpgradableSettings GetCurrentUpgradeInfo()
        {
            int currentLevel = _level.Value;
            for (int i = _upgradableSettings.Length - 1; 0 <= i; i--)
            {
                if (_upgradableSettings[i].StartLevel <= currentLevel)
                    return _upgradableSettings[i];
            }
            
            throw new System.Exception("There is no elements of UpgradeInfo!...");
        }

        public void Reset()
        {
            SetLevel(Info.InitLevel);
        }

        public virtual void MakeUpgrade()
        {
            SetLevel(_level.Value + 1);
        }

        private void SetLevel(int level)
        {
            _level.Value = level;
            _onUpgraded.Invoke(item => item.OnUpgrade(level));
        }

        public bool TryGetBalanceInfo(string name, out IBalanceInfo value)
        {
            return _balanceInfo.TryGetValue(name, out value);
        }

        public IEnumerator<IBalanceInfo> GetEnumerator()
        {
            return _balanceInfo.Values.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _balanceInfo.Values.AsEnumerable().GetEnumerator();
        }

        public IEnumerator<UpgradableSettings> GetUpgradeInfos()
        {
            return _upgradableSettings.AsEnumerable().GetEnumerator();
        }
    }
}
using System;

namespace HCEngine.Balance
{
    public interface IBalanceInfo
    {
        string Name { get; }
        float GetValue(int level);
    }
}

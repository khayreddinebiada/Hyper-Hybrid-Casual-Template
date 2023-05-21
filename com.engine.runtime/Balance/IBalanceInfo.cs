using System;

namespace Engine.Balance
{
    public interface IBalanceInfo : ICloneable
    {
        string Name { get; }
        float GetValue(int level);
    }
}

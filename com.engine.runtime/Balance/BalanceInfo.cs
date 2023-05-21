using UnityEngine;

namespace Engine.Balance
{
    public abstract class BalanceInfo : ScriptableObject, IBalanceInfo
    {
        [SerializeField] protected string _name = "Undefined";

        public bool IsTestMode = false;
        public float TestValue;

        public string Name => _name;

        public abstract float GetValue(int level);

        public abstract object Clone();
    }
}

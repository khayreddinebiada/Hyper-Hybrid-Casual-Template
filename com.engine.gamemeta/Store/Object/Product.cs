using HCEngine.Data;
using System;

namespace HCEngine.Store
{
    [Serializable]
    public class Product : IProduct
    {
        private ValueField<int> _stateField;

        protected string _name;
        protected string _category;
        protected ProductSettings _settings;

        public string Name => _name;
        public string Category => _category;

        public ProductSettings Settings => _settings;
        public ProductState State => (ProductState)_stateField.Value;


        public Product(string name, string category, ProductSettings settings, ValueField<int> field)
        {
            _name = name;
            _category = category;
            _stateField = field;
            _settings = settings;
        }

        public virtual bool Reward()
        {
            return ChangeState(ProductState.Unlocked);
        }

        public virtual void Deselect()
        {
            ChangeState(ProductState.Unlocked);
        }

        public virtual bool Select()
        {
            return ChangeState(ProductState.Selected);
        }

        protected bool ChangeState(ProductState newState)
        {
            if (State != newState)
            {
                _stateField.Value = (int)newState;
                return true;
            }

            return false;
        }
    }
}

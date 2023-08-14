using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using HCEngine.DI;
using HCEngine.Events;
using HCEngine.Diagnostics;
using HCEngine.Linq;

namespace HCEngine.Store
{
    [Serializable]
    public class Store : IStore, IAwake, IDependency
    {
        public Event<IProductUpdated> OnProductUpdated { get; private set; } = new Event<IProductUpdated>();

        [SerializeField, Identificator((int)Systems.Store)] private int _storeId;
        [SerializeField] private StoreInfo _info;

        private Product _selectedProduct;
        private Product[] _products;



        public IReadOnlyProduct this[string productName] => FindProduct(productName);

        public Store(StoreInfo info)
        {
            if (info.IsNull())
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
            Contract.EndContractBlock();

            _info = info;
        }

        public void Inject()
        {
            DIContainer.Register<IStore>(_storeId, this);
        }

        public void Awake()
        {
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            ProductConfig[] infos = _info.ProductInfos;
            _products = new Product[infos.Length];

            for (int i = 0; i < _products.Length; i++)
            {
                ProductConfig info = infos[i];
                
                _products[i] = new Product(info.Name, info.Cagegory, info.Settings, _info.ProductFields[i]);

                SetSelectedProduct(_products[i]);
            }
        }

        private void SetSelectedProduct(Product product)
        {
            if (product.State == ProductState.Selected)
                _selectedProduct = product;
        }

        private Product FindProduct(string productName)
        {
            return _products.FirstOrDefault(p => p.Name == productName);
        }

        private void InvokeCurrentProduct()
        {
            OnProductUpdated.Invoke(item => item.OnProductUpdated(new ReadOnlyProduct(_selectedProduct)));
        }

        public void DeselectCurrentProduct()
        {
            _selectedProduct.Deselect();
            _selectedProduct = null;

            InvokeCurrentProduct();
        }

        private bool SetProductSelected(Product product)
        {
            // Select new product
            if (product.Select())
            {

                _selectedProduct = product;
                InvokeCurrentProduct();

                return true;
            }

            return false;
        }

        private bool SelectProduct(Product product)
        {
            Contract.Assert(product != null);
            Contract.EndContractBlock();

            if (!AllowSelect(product))
                return false;

            // Deselect the old id.
            DeselectCurrentProduct();

            // Select new product
            return SetProductSelected(product);
        }


        private bool AllowSelect(Product product)
        {
            return product.State == ProductState.Unlocked;
        }

        public bool SelectProduct(string productName)
        {
            return SelectProduct(FindProduct(productName));
        }

        private bool AllowRewardProduct(Product product)
        {
            return product.State == ProductState.Locked;
        }

        public bool RewardProduct(string productName, bool autoSelect = true)
        {
            Product product = FindProduct(productName);
            Contract.Assert(product != null);
            Contract.EndContractBlock();

            if (!AllowRewardProduct(product))
                return false;

            bool isRewarded = product.Reward();

            if (isRewarded && autoSelect)
                return SelectProduct(product);

            return isRewarded;
        }


        public int GetTotalProducts()
        {
            return _products.Length;
        }

        public bool GetSelectedProduct(out IReadOnlyProduct product)
        {
            product = default;

            if (_selectedProduct != null)
            {
                product = new ReadOnlyProduct(_selectedProduct);
                return true;
            }

            return false;
        }

        public IEnumerator<IReadOnlyProduct> GetEnumerator()
        {
            return _products.Cast<IReadOnlyProduct>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return _products.GetEnumerator();
        }
    }
}

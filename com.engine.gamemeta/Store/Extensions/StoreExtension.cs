using System.Collections.Generic;
using System.Diagnostics;
using HCEngine.Data;
using System.Linq;

namespace HCEngine.Store
{
    public static class StoreExtension
    {
        public static void ResetStoreData(this StoreInfo storeInfo)
        {
            ValueField<int>[] dataInfos = new ValueField<int>[storeInfo.ProductInfos.Length];
            ProductConfig[] productInfos = storeInfo.ProductInfos;

            if (productInfos != null && productInfos.Length != 0)
            {
                for (int i = 0; i < dataInfos.Length; i++)
                {
                    dataInfos[i] = new ValueField<int>(productInfos[i].Name, storeInfo.GetInstanceID().ToString(), (int)((i == 0) ? ProductState.Selected : ProductState.Locked));
                }
            }

            storeInfo.ProductFields = dataInfos;

            int duplicates = productInfos.GroupBy(x => x.Name).Where(g => g.Count() > 1).Select(y => y.Key).Count();
            Debug.Assert(duplicates == 0);
        }

        public static void UnlockStoreData(this StoreInfo storeInfo)
        {
            ValueField<int>[] dataInfos = new ValueField<int>[storeInfo.ProductInfos.Length];
            ProductConfig[] productInfos = storeInfo.ProductInfos;

            if (productInfos != null && productInfos.Length != 0)
            {
                for (int i = 0; i < dataInfos.Length; i++)
                {
                    dataInfos[i] = new ValueField<int>(productInfos[i].Name, storeInfo.GetInstanceID().ToString(), (int)((i == 0) ? ProductState.Selected : ProductState.Unlocked));
                }
            }

            storeInfo.ProductFields = dataInfos;

            int duplicates = productInfos.GroupBy(x => x.Name).Where(g => g.Count() > 1).Select(y => y.Key).Count();
            Debug.Assert(duplicates == 0);
        }

        public static string[] GetProductNames(this Store store)
        {
            string[] names = new string[store.GetTotalProducts()];
            int i = 0;

            foreach (IReadOnlyProduct product in store)
            {
                names[i] = product.Name;
                i++;
            }

            return names;
        }

        public static IEnumerable<IReadOnlyProduct> GetProductsWithState(this Store store, ProductState state)
        {
            List<IReadOnlyProduct> names = new List<IReadOnlyProduct>();

            foreach (IReadOnlyProduct product in store)
            {
                if (product.State == state)
                    names.Add(product);
            }

            return names;
        }

        public static IEnumerable<IReadOnlyProduct> GetProductsWithCategory(this Store store, string category)
        {
            List<IReadOnlyProduct> names = new List<IReadOnlyProduct>();

            foreach (IReadOnlyProduct product in store)
            {
                if (product.Category == category)
                    names.Add(product);
            }

            return names;
        }

        public static IEnumerable<IReadOnlyProduct> GetProductsWhere(this Store store, System.Predicate<IReadOnlyProduct> predicate)
        {
            List<IReadOnlyProduct> names = new List<IReadOnlyProduct>();

            foreach (IReadOnlyProduct product in store)
            {
                if (predicate.Invoke(product))
                    names.Add(product);
            }

            return names;
        }
    }
}

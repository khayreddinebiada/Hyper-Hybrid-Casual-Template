using HCEditor;
using HCEngine.Store;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class TestStore
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestStorePass1()
    {
        HeadTemplateEditor.ResetAllData();

        StoreInfo info = AssetHelper.FindScribtableObjectOfType<StoreInfo>();
        Assert.IsNotNull(info);

        Store store = new Store(info);
        store.Awake();
        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);

        string[] productsNames = store.GetProductNames();

        store.RewardProduct(productsNames[1], true);
        Assert.IsTrue(store[productsNames[1]].State == ProductState.Selected);

        store.RewardProduct(productsNames[2], false);
        Assert.IsTrue(store[productsNames[2]].State == ProductState.Unlocked);


        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);
        Assert.IsFalse(store.RewardProduct(productsNames[2], false));
        Assert.IsFalse(store.RewardProduct(productsNames[2], true));


        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, productsNames.Length);
            if (store.RewardProduct(productsNames[index], false))
                Assert.IsTrue(store[productsNames[index]].State == ProductState.Unlocked);
        }
        
        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, productsNames.Length);
            if (store.RewardProduct(productsNames[index]))
                Assert.IsTrue(store[productsNames[index]].State == ProductState.Selected);
        }
        
        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);

        foreach (IReadOnlyProduct product in store)
        {
            store.RewardProduct(product.Name);
        }

        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);
        
        foreach (IReadOnlyProduct product in store)
        {
            Assert.IsFalse(store.RewardProduct(product.Name));
        }

        Assert.IsTrue(store.GetProductsWithState(ProductState.Selected).Count() <= 1);

        foreach (IReadOnlyProduct product in store)
        {
            Assert.IsTrue(store.SelectProduct(product.Name));
        }

        Assert.IsTrue(store.GetSelectedProduct(out IReadOnlyProduct outputPorduct1));
        Assert.IsTrue(outputPorduct1.State == ProductState.Selected);
        store.DeselectCurrentProduct();
        Assert.IsFalse(store.GetSelectedProduct(out _));
    }
}

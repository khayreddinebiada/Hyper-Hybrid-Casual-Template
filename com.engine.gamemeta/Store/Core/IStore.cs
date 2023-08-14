using HCEngine.Events;
using System.Collections.Generic;

namespace HCEngine.Store
{
    public interface IStore : IEnumerable<IReadOnlyProduct>
    {
        Event<IProductUpdated> OnProductUpdated { get; }

        IReadOnlyProduct this[string productName] { get; }

        bool SelectProduct(string productName);
        bool RewardProduct(string productName, bool autoSelect = true);

        int GetTotalProducts();

        bool GetSelectedProduct(out IReadOnlyProduct product);
    }
}

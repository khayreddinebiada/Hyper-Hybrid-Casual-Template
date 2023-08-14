using HCEngine.Events;

namespace HCEngine.Currency
{
    public interface ICurrency
    {
        IEventSubscribe<ICurrencyUpdated> OnCurrencyUpdated { get; }

        int TotalCoins { get; }

        bool AddCurrency(int amount);

        bool RemoveCurrency(int amount);
    }
}

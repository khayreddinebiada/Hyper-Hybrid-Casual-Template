namespace HCEngine.Currency
{
    public static class CurrencyExtension
    {
        public static void ResetCurrencyData(this CurrencyInfo currencyInfo, string fileName)
        {
            currencyInfo.CurrentValue = new Data.ValueField<int>("CurrentValue", fileName, currencyInfo.InitValue);
        }

        public static void UnlockCurrencyData(this CurrencyInfo currencyInfo, string fileName)
        {
            currencyInfo.CurrentValue = new Data.ValueField<int>("CurrentValue", fileName, int.MaxValue);
        }
    }
}

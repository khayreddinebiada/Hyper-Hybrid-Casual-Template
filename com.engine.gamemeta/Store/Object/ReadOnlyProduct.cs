namespace HCEngine.Store
{
    public struct ReadOnlyProduct : IReadOnlyProduct
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public ProductState State { get; set; }
        public ProductSettings Settings { get; set; }

        public ReadOnlyProduct(string name, string category, ProductSettings settings, ProductState state)
        {
            Name = name;
            Category = category;
            Settings = settings;

            State = state;
        }

        public ReadOnlyProduct(IProduct product) : this(product.Name, product.Category, product.Settings, product.State) { }
    }
}
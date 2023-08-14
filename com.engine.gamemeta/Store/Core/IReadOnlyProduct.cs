namespace HCEngine.Store
{
    public interface IReadOnlyProduct
    {
        ProductState State { get; }

        ProductSettings Settings { get; }

        string Name { get; }
        string Category { get; }
    }
}
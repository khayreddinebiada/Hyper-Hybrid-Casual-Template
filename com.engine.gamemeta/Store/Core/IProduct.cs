namespace HCEngine.Store
{
    public interface IProduct : IReadOnlyProduct
    {
        void Deselect();
        bool Reward();
        bool Select();
    }
}

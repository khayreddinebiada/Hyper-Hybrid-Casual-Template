namespace HCEngine.Upgrade
{
    public interface IUpgradeableCard : IUpgradeable
    {
        bool IsAbleUpgrade { get; }
        int CardsAvailable { get; }
        int CardsRequired { get; }

        void AddCard(int count);
    }
}
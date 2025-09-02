public interface IEconomyService
{
    long Cash { get; }
    bool CanSpend(long amount);
    bool Spend(long amount);
    void Add(long amount);
    void LoadFromSave();
}

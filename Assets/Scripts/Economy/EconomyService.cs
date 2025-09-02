public class EconomyService : IEconomyService
{
    private readonly ISaveService _save;

    public long Cash => _save.Data.Player.Cash;

    public EconomyService(ISaveService save)
    {
        _save = save;
    }

    public void LoadFromSave() { }

    public bool CanSpend(long amount)
    {
        return amount <= _save.Data.Player.Cash;
    }

    public bool Spend(long amount)
    {
        if (!CanSpend(amount)) return false;
        _save.Data.Player.Cash -= amount;
        return true;
    }

    public void Add(long amount)
    {
        _save.Data.Player.Cash += amount;
    }
}

using System.Collections.Generic;

public class BuildingService : IBuildingService
{
    private readonly ISaveService _save;
    private readonly IEconomyService _economy;
    private readonly Dictionary<string, SceneSlotSave> _cache = new();

    public BuildingService(ISaveService save, IEconomyService economy)
    {
        _save = save;
        _economy = economy;
    }

    public void LoadFromSave()
    {
        _cache.Clear();
        var list = _save.Data.SceneSlots;
        for (int i = 0; i < list.Count; i++)
            _cache[list[i].SlotId] = list[i];
    }

    public bool IsBuilt(string slotId)
    {
        if (_cache.TryGetValue(slotId, out var s)) return s.Built;
        return false;
    }

    public bool TryBuy(string slotId, long price)
    {
        if (IsBuilt(slotId)) return false;
        if (!_economy.Spend(price)) return false;

        if (_cache.TryGetValue(slotId, out var s))
        {
            s.Built = true;
            if (s.Level <= 0) s.Level = 1;
        }
        else
        {
            s = new SceneSlotSave { SlotId = slotId, Built = true, Level = 1 };
            _save.Data.SceneSlots.Add(s);
            _cache[slotId] = s;
        }

        _save.Save();
        return true;
    }
}

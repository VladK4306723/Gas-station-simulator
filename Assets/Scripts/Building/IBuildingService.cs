public interface IBuildingService
{
    bool IsBuilt(string slotId);
    bool TryBuy(string slotId, long price);
    void LoadFromSave();
}

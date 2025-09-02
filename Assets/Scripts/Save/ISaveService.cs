public interface ISaveService
{
    SaveData Data { get; }
    void LoadOrCreate();
    void Save();
}

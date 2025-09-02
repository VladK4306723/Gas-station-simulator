using UnityEngine;
using System.IO;

public class JsonSaveService : ISaveService
{
    public SaveData Data { get; private set; }

    private string PathFile => System.IO.Path.Combine(Application.persistentDataPath, "save.json");

    public void LoadOrCreate()
    {
        if (File.Exists(PathFile))
        {
            var json = File.ReadAllText(PathFile);
            Data = JsonUtility.FromJson<SaveData>(json);
            if (Data == null) Data = new SaveData();
        }
        else
        {
            Data = new SaveData();
            Save();
        }
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(PathFile, json);
    }
}

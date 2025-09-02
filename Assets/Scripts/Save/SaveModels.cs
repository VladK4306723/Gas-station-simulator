using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerData Player = new PlayerData();
    public List<SceneSlotSave> SceneSlots = new List<SceneSlotSave>();
}

[Serializable]
public class PlayerData
{
    public long Cash = 1000;
}

[Serializable]
public class SceneSlotSave
{
    public string SlotId;
    public bool Built;
    public int Level;
}

using UnityEngine;

public class GameEntry : MonoBehaviour
{
    public static ServiceContainer Container { get; private set; }

    private void Awake()
    {
        Container = new ServiceContainer();

        var save = new JsonSaveService();
        Container.Register<ISaveService>(save);

        var economy = new EconomyService(save);
        Container.Register<IEconomyService>(economy);

        var building = new BuildingService(save, economy);
        Container.Register<IBuildingService>(building);

        save.LoadOrCreate();
        economy.LoadFromSave();
        building.LoadFromSave();
    }
}

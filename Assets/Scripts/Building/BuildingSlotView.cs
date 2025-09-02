using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingSlotView : MonoBehaviour
{
    [SerializeField] private string slotId = "slot_01";
    [SerializeField] private long price = 100;
    [SerializeField] private GameObject placeholderRoot;
    [SerializeField] private GameObject buildingRoot;
    [SerializeField] private GameObject clickableTarget;


    private IBuildingService _building;

    private void Start()
    {
        _building = GameEntry.Container.Resolve<IBuildingService>();

        Apply(_building.IsBuilt(slotId));
    }
    private void OnEnable()
    {
        if (clickableTarget == null) return;

        var trigger = clickableTarget.GetComponent<EventTrigger>() ?? clickableTarget.AddComponent<EventTrigger>();
        trigger.triggers ??= new List<EventTrigger.Entry>();

        var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        entry.callback.AddListener(e => OnPointerClick(e as PointerEventData));
        trigger.triggers.Add(entry);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_building.TryBuy(slotId, price))
            Apply(true);
    }

    private void Apply(bool built)
    {
        if (placeholderRoot != null) placeholderRoot.SetActive(!built);
        if (buildingRoot != null) buildingRoot.SetActive(built);
    }
}

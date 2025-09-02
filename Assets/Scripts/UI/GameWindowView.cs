using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameWindowView : MonoBehaviour
{
    [SerializeField] private TMP_Text cashText;
    private IEconomyService _economy;

    private void Start()
    {
        _economy = GameEntry.Container.Resolve<IEconomyService>();
        InvokeRepeating(nameof(Refresh), 0f, 0.2f);
    }

    private void Refresh()
    {
        if (cashText != null) cashText.text = $"$ {_economy.Cash}";
    }
}

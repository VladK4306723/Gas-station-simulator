using UnityEngine;
using System.Collections;

public class AutoSaver : MonoBehaviour
{
    [SerializeField] private float intervalSeconds = 60f;
    private ISaveService _save;

    private void Start()
    {
        _save = GameEntry.Container.Resolve<ISaveService>();
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        var wait = new WaitForSeconds(intervalSeconds);
        while (true)
        {
            yield return wait;
            _save.Save();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) _save.Save();
    }

    private void OnApplicationQuit()
    {
        _save.Save();
    }
}

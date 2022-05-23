using TMPro;
using UnityEngine;

public class RewireUIManager : MonoBehaviour
{
    public GameObject successText;
    private void Start()
    {
        WireSpawner spawner = GetComponent<WireSpawner>();
        spawner.GameSuccess += WireSpwawner_GameSuccess;
    }

    private void WireSpwawner_GameSuccess(bool isSuccess)
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
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
        successText.SetActive(true);

        TextMeshProUGUI text = successText.GetComponent<TextMeshProUGUI>();
        if (isSuccess)
        {
            text.color = Color.green;
            text.text = "SUCCESS";
        }
        else
        {
            text.color = Color.red;
            text.text = "FAILURE";
        }
    }
}

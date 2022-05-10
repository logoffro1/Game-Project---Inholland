using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameMode GameModeOfButton;
    public TextMeshProUGUI NormalText;
    public TextMeshProUGUI ChillText;
    public TextMeshProUGUI CrazyText;
    public TextMeshProUGUI DefaultText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        NormalText.enabled = false;
        ChillText.enabled = false;
        CrazyText.enabled = false;
        DefaultText.enabled = false;

        switch (GameModeOfButton)
        {
            case GameMode.Chill:
                ChillText.enabled = true;
                break;
            case GameMode.Crazy:
                CrazyText.enabled = true;
                break;
            case GameMode.Normal:
                NormalText.enabled = true;
                break;

        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        NormalText.enabled = false;
        ChillText.enabled = false;
        CrazyText.enabled = false;
        DefaultText.enabled = true;
    }
}

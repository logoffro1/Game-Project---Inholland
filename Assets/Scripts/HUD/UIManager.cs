using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hoverText;
    private static UIManager _instance = null;
    public static UIManager Instance { get { return _instance; } }
    private Canvas canvas;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        canvas = GetComponent<Canvas>();
    }
    public void SetHoverText(string text)
    {
        if (text == null)
        {
            hoverText.text = "";
            return;
        }
        hoverText.text = $"(E) {text}";
    }
    public void ChangeCanvasShown()
    {
        canvas.enabled = !canvas.enabled;
    }
}

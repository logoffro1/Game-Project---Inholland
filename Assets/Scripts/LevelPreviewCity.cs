using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class LevelPreviewCity : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public TextMeshProUGUI playText;
    public TextMeshProUGUI descriptionText;
    public Texture2D handIcon;

    private string mapName = "Alkmaar City Center";
    private string mapDescription = "Pro/Cons of playing the map";
    private string objectives = "Here we write what we have to do/ point of the game etc";
    private void Start()
    {
        SetDescription();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(handIcon,Vector2.zero,CursorMode.Auto);
        StartCoroutine(HoverAnim());
    }
    private IEnumerator HoverAnim()
    {
        playText.enabled = true;
        descriptionText.enabled = true;
        Vector3 scale = transform.localScale;
        while (true)
        {
            scale = Vector3.Lerp(scale, new Vector3(1.15f, 1.15f, 1.15f), 0.1f);
            transform.localScale = scale;
            yield return new WaitForSeconds(0.005f);

            if (transform.localScale.x >= 1.14f) break;

        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        transform.localScale = new Vector3(1f, 1f, 1f);
        playText.enabled = false;
        descriptionText.enabled = false;
    }
    private void SetDescription() {
        descriptionText.text = $"Map: {mapName} \n\nDescription: {mapDescription}\nObjectives: {objectives}\n\nTimes played: 0\nBest score: 0";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.LoadScene("GameUKDay");
    }
}

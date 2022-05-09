using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
public class LevelPreviewCity : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public TextMeshProUGUI playText;
    public TextMeshProUGUI descriptionText;
    public Texture2D handIcon;

    private string mapName = "Alkmaar City Center";
    private string mapDescription = "Explore the beautiful city of Alkmaar";
    private string objectives = "Clean the city and bring the sustainability above 80%!";

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
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            LevelManager.Instance.LoadScenePhoton("GameUKDay",true);
        }

    }
}

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
public class LevelPreviewCity : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public DistrictEnum District;

    public TextMeshProUGUI districtName;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI descriptionText;
    public GameObject image;
    public Texture2D handIcon;

    public GameObject startButton;
    public string sceneName;

    private TextMeshProUGUI middleSelectText;
    private TextMeshProUGUI topSelectText;
    private List<LevelPreviewCity> otherLevels;
    private OverallDistrictScreen overallDistrictScreen;

    private bool firstClick = false;

    private void Start()
    {
        overallDistrictScreen = GetComponentInParent<OverallDistrictScreen>();
        if (overallDistrictScreen.PlayerData == null) overallDistrictScreen.PlayerData = FindObjectOfType<PlayerData>();
        middleSelectText = overallDistrictScreen.middleSelectText;
        topSelectText = overallDistrictScreen.topSelectText;
        otherLevels = transform.parent.GetComponentsInChildren<LevelPreviewCity>().Where(x => x.districtName != this.districtName).ToList();

        //SetPercentage
        TextMeshProUGUI percentageText = GetComponentInChildren<TextMeshProUGUI>();
        float percentage = 50;
        switch(District)
        {
            case DistrictEnum.CityCenter:
                percentage = overallDistrictScreen.PlayerData.CityCenterSustainability;
                percentageText.text = overallDistrictScreen.ConvertFloatPercentageToString(percentage);
                break;
            case DistrictEnum.FarmLand:
                percentage = overallDistrictScreen.PlayerData.FarmSustainability;
                percentageText.text = overallDistrictScreen.ConvertFloatPercentageToString(percentage);
                break;
            case DistrictEnum.ThirdMap:
                percentage = overallDistrictScreen.PlayerData.LastMapSustainability;
                percentageText.text = overallDistrictScreen.ConvertFloatPercentageToString(percentage);
                break;
        }

        GetComponent<Image>().color = overallDistrictScreen.mapGradient.Evaluate(percentage);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(handIcon,Vector2.zero,CursorMode.Auto);

        foreach (LevelPreviewCity level in otherLevels)
        {
            level.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        StartCoroutine(HoverAnim());
    }
    private IEnumerator HoverAnim()
    {

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
        //EnableAllText(false);
        //GetComponentInChildren<Button>().gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
 

        //disable
        foreach (LevelPreviewCity level in otherLevels)
        {
            level.EnableAllText(false);
            level.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Enabnle this one
        EnableAllText(true);
        middleSelectText.enabled = false;
        topSelectText.enabled = true;
        startButton.SetActive(false);
        foreach(Transform child in startButton.transform)
        {
            child.gameObject.GetComponent<DistrictLoadScene>().SceneName = sceneName;
        }
        startButton.SetActive(true);
        overallDistrictScreen.PlayerData.IsInDistrict = District;
        firstClick = true;
    }

    public void EnableAllText(bool value)
    {
        districtName.enabled = value;
        goalText.enabled = value;
        descriptionText.enabled = value;
        image.SetActive(value);
    }
}

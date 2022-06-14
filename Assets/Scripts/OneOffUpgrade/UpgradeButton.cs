using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

//Interaction with the upgrade buttons
public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private OneOffUpgradesEnum upgrade;
    public OneOffUpgradesEnum Upgrade { get { return upgrade; } set { upgrade = value; } }

    private UpgradeUI ui;
    private Player player;
    private UpgradeManager manager;
    public Text defaultExplanationText;
    private TextMeshProUGUI description;

    private void Start()
    {
        ui = GetComponentInParent<UpgradeUI>();
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<UpgradeManager>();
        description = gameObject.GetComponentInParent<UpgradeUI>().upgradeDescriptionText;
    }

    //Normal upgrdae button was clicked
    public void ClickButton()
    {
        //shut off UI
        ui.TurnOff();

        //add upgrade to player
        player.GetUpgrade(upgrade).PerformLevelUp();

        manager.UpgradeSessionFInished();

    }

    //Adds points if the last option was clicked
    public void ClickPointsButton()
    {
        ui.TurnOff();
        ProgressBar.Instance.ChangeSustainibility(3f, true);
        manager.UpgradeSessionFInished();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.text = player.GetUpgrade(upgrade).Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.text = defaultExplanationText.text;
    }
}

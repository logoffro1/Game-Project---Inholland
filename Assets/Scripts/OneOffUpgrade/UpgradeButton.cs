using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private OneOffUpgradesEnum upgrade;
    public OneOffUpgradesEnum Upgrade { get { return upgrade; } set { upgrade = value; } }

    private UpgradeUI ui;
    private Player player;
    private UpgradeManager manager;

    //
    private TextMeshProUGUI description;
    private void Start()
    {
        ui = GetComponentInParent<UpgradeUI>();
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<UpgradeManager>();
        description = gameObject.GetComponentInParent<UpgradeUI>().upgradeDescriptionText;
    }

    public void ClickButton()
    {
        //shut off UI
        ui.TurnOff();

        //add upgrade to playuer
        player.GetUpgrade(upgrade).PerformLevelUp();

        manager.UpgradeSessionFInished();

    }

    public void ClickPointsButton()
    {
        ui.TurnOff();
        ProgressBar.Instance.SetSlideValue(ProgressBar.Instance.GetSlideValue() + 3f);
        manager.UpgradeSessionFInished();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.text = player.GetUpgrade(upgrade).Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.text = "You can choose any upgrade to help you out on your mission!";
    }
}

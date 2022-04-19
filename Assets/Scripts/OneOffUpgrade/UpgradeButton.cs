using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour 
{
    private OneOffUpgradesEnum upgrade;
    public OneOffUpgradesEnum Upgrade { get { return upgrade; } set { upgrade = value; } }

    private UpgradeUI ui;
    private Player player;
    private UpgradeManager manager;

    public string Text;

    private void Start()
    {
        ui = GetComponentInParent<UpgradeUI>();
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<UpgradeManager>();
        Text = upgrade.ToString();
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


}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;

//regulated the ui of the upgrade
public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI[] buttonOptions;
    public LocalizedString[] localizedStrings;

    public void TurnOff()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        SetUpUi(false);

    }

    public void TurnOn()
    {
        GenerateUpgradeOptions();
        this.gameObject.GetComponent<Canvas>().enabled = true;
        SetUpUi(true);
    }

    private void GenerateUpgradeOptions()
    {
        //Gets the player
        Player player = FindObjectsOfType<Player>().Where(x => x.Host).FirstOrDefault(); 

        //Gets the player's upgrades list
        List<OneOffUpgrade> list = new List<OneOffUpgrade>(FindObjectsOfType<Player>().Where(x => x.Host).FirstOrDefault().OneOffUpgradeList);

        //For each upgrade button, assign a button to it with the correct level and correct feedback UI
        foreach(TextMeshProUGUI textButton in buttonOptions)
        {
            //Gets random upgrade from list and remove from tmp list afterwards
            int randomIndex = Random.Range(0, list.Count);
            OneOffUpgrade upgrade = list[randomIndex];
            list.RemoveAt(randomIndex);
            UnityEngine.UI.Button button = textButton.gameObject.GetComponentInParent<UnityEngine.UI.Button>();

            //If upgrade level is at max level,change the UI and back that button can not be clicked
            //Else, have a nice image
            if (upgrade.Level >= upgrade.MaxLevel)
            {
                //Editting the button itself
                button.interactable = false;
                Color color = textButton.color;
                color.a = 0.6f;
                textButton.color = color;

                ColorBlock colorBlock = ColorBlock.defaultColorBlock;
                colorBlock.normalColor = Color.gray;
                button.colors = colorBlock;

                //Text
                textButton.text = string.Format($"{upgrade.Title}: LVL MAX");
            }
            else
            {
                button.interactable = true;
                Color color = textButton.color;
                color.a = 1f;
                textButton.color = color;

                ColorBlock colorBlock = ColorBlock.defaultColorBlock;
                colorBlock.normalColor = Color.white;
                button.colors = colorBlock;

                //Text
                textButton.text = string.Format($"{upgrade.Title}: LVL {upgrade.Level + 1}");
                UpgradeButton u = textButton.gameObject.GetComponentInParent<UpgradeButton>();
                u.Upgrade = upgrade.Upgrade;
            }

            //Set the correct description
            upgradeDescriptionText.text = upgrade.Description;
        }
    }

    //Set to the correct UI and correct mouse movement
    private void SetUpUi(bool setUp)
    {
        MiniGameManager minigameManager = FindObjectOfType<MiniGameManager>();
        minigameManager.FreezeScreen(setUp);
        Cursor.visible = setUp;
        if (setUp)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}

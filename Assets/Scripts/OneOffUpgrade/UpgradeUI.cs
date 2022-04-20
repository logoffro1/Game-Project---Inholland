using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI[] buttonOptions;

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

        //freeze
    }

    private void GenerateUpgradeOptions()
    {
        Player player = FindObjectsOfType<Player>().Where(x => x.Host).FirstOrDefault(); //chck if done correctlyu
        List<OneOffUpgrade> list = new List<OneOffUpgrade>(FindObjectsOfType<Player>().Where(x => x.Host).FirstOrDefault().OneOffUpgradeList);
        foreach(TextMeshProUGUI textButton in buttonOptions)
        {
            int randomIndex = Random.Range(0, list.Count);
            OneOffUpgrade upgrade = list[randomIndex];
            list.RemoveAt(randomIndex);
            UnityEngine.UI.Button button = textButton.gameObject.GetComponentInParent<UnityEngine.UI.Button>();

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
            upgradeDescriptionText.text = upgrade.Description;
        }
    }

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

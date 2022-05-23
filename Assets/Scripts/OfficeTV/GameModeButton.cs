using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

//For when the player pressed the gamnemode buttons
public class GameModeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameMode GameModeOfButton;
    public TextMeshProUGUI NormalText;
    public TextMeshProUGUI ChillText;
    public TextMeshProUGUI CrazyText;
    public TextMeshProUGUI DefaultText;
    private PlayerData playerData;

    //Sets for multiplayer
    private void Start()
    {
        foreach (PlayerData pd in FindObjectsOfType<PlayerData>())
        {
            if (pd.photonView.IsMine)
            {
                playerData = pd;
            }
        }
    }


    //When mouse hovers above a mode, change the text
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
                playerData.GoalText = ChillText.text;
                break;
            case GameMode.Crazy:
                CrazyText.enabled = true;
                playerData.GoalText = CrazyText.text;
                break;
            case GameMode.Normal:
                NormalText.enabled = true;
                playerData.GoalText = NormalText.text;
                break;
        }
    }

    //When is not hovering, reset everything
    public void OnPointerExit(PointerEventData eventData)
    {
        NormalText.enabled = false;
        ChillText.enabled = false;
        CrazyText.enabled = false;
        DefaultText.enabled = true;
    }
}

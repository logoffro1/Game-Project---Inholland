using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour // players information in the office
{
    [SerializeField] private TextMeshProUGUI text; 
    [SerializeField] private Image hostIndicator;

    public Photon.Realtime.Player Player { get; private set; }

    public void SetPlayerInfo(int nr, Photon.Realtime.Player player) // set information to display on screen
    {
        if (player.IsMasterClient) // if the client is the host, show host icon next to name
            hostIndicator.enabled = true;
        else
            hostIndicator.enabled = false;

        Player = player;
        text.text = $"{nr} | {player.NickName}";
    }
}

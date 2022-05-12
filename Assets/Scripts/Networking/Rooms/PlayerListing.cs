using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image hostIndicator;

    public Photon.Realtime.Player Player { get; private set; }

    public void SetPlayerInfo(int nr, Photon.Realtime.Player player)
    {
        if (player.IsMasterClient)
            hostIndicator.enabled = true;
        else
            hostIndicator.enabled = false;
        Player = player;
        text.text = $"{nr} | {player.NickName}";
    }
}

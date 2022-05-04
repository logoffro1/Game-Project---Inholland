using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public Photon.Realtime.Player player;

    public void SetPlayerInfo(Photon.Realtime.Player player)
    {
        this.player = player;
        this.text.text = player.NickName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public RoomInfo RoomInfo { get; private set; }

public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = $"{roomInfo.Name}  |  {roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }
}

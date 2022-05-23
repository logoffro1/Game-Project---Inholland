using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListing : MonoBehaviour // store room listing information
{
    [SerializeField] TextMeshProUGUI text; // show on screen

    public RoomInfo RoomInfo { get; private set; }

public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = $"{roomInfo.Name}  |  {roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoomsListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListing roomListing;
    [SerializeField] private Transform content;

    private List<RoomListing> listings = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("UPDATE ROOM");
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList) //removed room
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }

            }
            else //added room
            {
                RoomListing listing = Instantiate(roomListing, content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    listings.Add(listing);
                }
            }

        }
    }
    public void OnClick_JoinGame(RoomListing roomListing)
    {
        if (!PhotonNetwork.IsConnected) return;

        if (roomListing.RoomInfo.PlayerCount < roomListing.RoomInfo.MaxPlayers)
        {
            PhotonNetwork.JoinRoom(roomListing.RoomInfo.Name);
            LevelManager.Instance.LoadScenePhoton("Office",false);
        }

    }
    public override void OnJoinedRoom()
    {

    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Entered room...");

    }


}

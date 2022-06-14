using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
public class RoomsListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListing roomListing;
    [SerializeField] private Transform content;

    private List<RoomListing> listings = new List<RoomListing>();
    private List<RoomInfo> roomList;

    public override void OnRoomListUpdate(List<RoomInfo> roomList) // whenever new room is added / removed
    {
        this.roomList = roomList;
        UpdateRoom();
    }
    private void UpdateRoom() // Update room listings
    {
        listings.Clear();
        foreach (RoomInfo info in this.roomList)
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
    public void OnClick_JoinGame(RoomListing roomListing) // when client joins another room
    {
        if (!PhotonNetwork.IsConnected) return;

        if (roomListing.RoomInfo.PlayerCount < roomListing.RoomInfo.MaxPlayers)
        { // if there is room, load the new scene and join room
            PhotonNetwork.JoinRoom(roomListing.RoomInfo.Name);
            LevelManager.Instance.LoadScenePhoton("NewOffice",false);
        }
        

    }
}

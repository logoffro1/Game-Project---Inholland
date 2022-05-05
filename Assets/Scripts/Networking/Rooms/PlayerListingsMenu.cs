using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerListing playerListing;
    [SerializeField] private Transform content;

    private List<PlayerListing> listings = new List<PlayerListing>();
    private void Start()
    {
        int count = 1;
        if(PhotonNetwork.CurrentRoom != null)
        {
            foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
            {

                PlayerListing listing = Instantiate(this.playerListing, content);
                if (listing != null)
                {
                    listing.SetPlayerInfo(count, player.Value);
                    listings.Add(listing);
                    count++;
                }
            }
        }

    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        foreach(PlayerListing l in listings)
        {
            Destroy(l.gameObject);
        }
        listings.Clear();
        int count = 1;
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {

            PlayerListing listing = Instantiate(this.playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(count, player.Value);
                listings.Add(listing);
                count++;
            }
        }
    }
    public override void OnLeftRoom()
    {
        content.DestroyChildren();
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks // the players shown on screen in the office/lobby
{
    [SerializeField] private PlayerListing playerListing;
    [SerializeField] private Transform content;

    private List<PlayerListing> listings = new List<PlayerListing>();
    private void Start()
    {
        int count = 1;
        if (PhotonNetwork.CurrentRoom != null)
        {
            // loop through all players currently in the room
            foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                // set the player listing information and add to the list
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
        // update players listing when new player joins

        // destroy and clear all listings
        foreach (PlayerListing l in listings)
        {
            Destroy(l.gameObject);
        }
        listings.Clear();
        int count = 1;
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {


            // set the player listing information and add to the list
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
        content.DestroyChildren(); // clear the employees canvas
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    { // update the players listing when player leaves

        int index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
}

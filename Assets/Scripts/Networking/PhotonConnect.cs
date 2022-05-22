using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // always sync the client scenes with the master client's scene

        // set the Photon Settings with the scriptable object game settings
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster() // when connected to server join lobby
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause) // on disconnected from server
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
    }
}

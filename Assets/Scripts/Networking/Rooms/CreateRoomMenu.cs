using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI roomName;
    [SerializeField] TextMeshProUGUI roomPassword;
    public void OnClick_CreateRoom() // create room on button click
    {
        if (!PhotonNetwork.IsConnected) return;
        if (roomName.text.Length <= 1) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;


        //create new room on the network
        PhotonNetwork.CreateRoom(roomName.text, options, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        LevelManager.Instance.LoadScenePhoton("NewOffice",false); // load client into the office
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed " + message, this);
    }
}

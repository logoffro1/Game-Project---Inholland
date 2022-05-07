using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI roomName;
    [SerializeField] TextMeshProUGUI roomPassword;
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (roomName.text.Length <= 1) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(roomName.text, options, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.", this);
        LevelManager.Instance.LoadScene("Office");
        LevelManager.Instance.CoroutineLoading();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed " + message, this);
    }
}

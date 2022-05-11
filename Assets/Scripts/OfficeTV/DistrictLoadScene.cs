using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class DistrictLoadScene : MonoBehaviour
{
    [HideInInspector] public string SceneName;

    private PlayerData playerData;
    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }
    private void LoadMyScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            LevelManager.Instance.LoadScenePhoton(SceneName, true);
        }
    }

    public void ChoseNormalMode()
    {
        playerData.IsInGameMode = GameMode.Normal;
        LoadMyScene();
    }

    public void ChoseChillMode()
    {
        playerData.IsInGameMode = GameMode.Chill;
        LoadMyScene();
    }

    public void ChoseCrazyMode()
    {
        playerData.IsInGameMode = GameMode.Crazy;
        LoadMyScene();
    }


}

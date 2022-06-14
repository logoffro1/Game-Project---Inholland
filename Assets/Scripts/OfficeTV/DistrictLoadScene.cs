using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

//Loads the proper scene when clicking the correct district in the map selection
public class DistrictLoadScene : MonoBehaviour
{
    [HideInInspector] public string SceneName;

    private PlayerData[] playerDatas;

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
        if (PhotonNetwork.IsMasterClient)
        {
            playerDatas = FindObjectsOfType<PlayerData>();
            foreach (PlayerData pd in playerDatas)
                pd.IsInGameMode = GameMode.Normal;
            LoadMyScene();
        }
    }

    public void ChoseChillMode()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerDatas = FindObjectsOfType<PlayerData>();
            foreach (PlayerData pd in playerDatas)
                pd.IsInGameMode = GameMode.Chill;
            LoadMyScene();
        }
        
    }

    public void ChoseCrazyMode()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerDatas = FindObjectsOfType<PlayerData>();
            foreach (PlayerData pd in playerDatas)
                pd.IsInGameMode = GameMode.Crazy;
            LoadMyScene();
        }
    }


}

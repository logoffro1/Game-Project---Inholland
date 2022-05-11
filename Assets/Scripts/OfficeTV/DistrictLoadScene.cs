using UnityEngine;
using UnityEngine.SceneManagement;


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
        LevelManager.Instance.LoadScene(SceneName);
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

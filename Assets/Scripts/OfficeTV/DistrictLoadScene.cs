using UnityEngine;
using UnityEngine.SceneManagement;


public class DistrictLoadScene : MonoBehaviour
{
    public string SceneName;
    public void LoadMyScene()
    {
        LevelManager.Instance.LoadScene(SceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using Photon.Pun;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loadCanvas;
    [SerializeField] private Slider progressBar;

    private float target;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void InitProgressBar()
    {
        target = 0;
        progressBar.value = 0;
        progressBar.minValue = 0;
        progressBar.maxValue = 1;
    }
    public async void LoadScene(string sceneName)
    {
        target = 0f;
        if (!PhotonNetwork.IsMasterClient) return;
        // var scene = SceneManager.LoadSceneAsync(sceneName);
        // scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);
        InitProgressBar();

        // scene.allowSceneActivation = true;

        PhotonNetwork.LoadLevel(sceneName);
        while(target < 1f)
        {
            await Task.Delay(500);
            target = PhotonNetwork.LevelLoadingProgress;
        }
        loadCanvas.SetActive(false);

    }
    private void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, target, 3 * Time.deltaTime);
    }
}

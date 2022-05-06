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

        // var scene = SceneManager.LoadSceneAsync(sceneName);
        // scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);
        InitProgressBar();

        do
        {
            await Task.Delay(500);
            target += Random.Range(0.2f,0.5f);
        } while (target < 1f);

       // scene.allowSceneActivation = true;
        loadCanvas.SetActive(false);

        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(sceneName);
    }
    private void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, target, 3 * Time.deltaTime);
    }
}

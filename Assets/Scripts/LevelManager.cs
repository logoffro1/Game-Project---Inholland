using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
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
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);
        InitProgressBar();

        do
        {
            await Task.Delay(500);
            Debug.Log(scene.progress);
            target = scene.progress;
            await Task.Delay(1000);
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        loadCanvas.SetActive(false);
    }
    private void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, target, 3 * Time.deltaTime);
    }
}

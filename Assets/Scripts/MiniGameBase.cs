using UnityEngine;
using TMPro;
public class MiniGameBase : MonoBehaviour
{

    protected int sustainabilityPoints = 10;
    
    public bool IsPlaying { get; set; } = true;
    public TextMeshProUGUI successText;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    protected void GameOver() //remove the duplicate
    {
        ProgressBar.Instance.ChangeSustainibility(-sustainabilityPoints);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        ChangeSuccessText(false);
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    protected void GameWon() //remove the duplicate
    {
        ProgressBar.Instance.ChangeSustainibility(sustainabilityPoints);
        ChangeSuccessText(true);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void ChangeSuccessText(bool successful)
    {
        successText.enabled = true;
        if (successful)
        {
            successText.color = Color.green;
            successText.text = "SUCCESS";
            return;
        }
        successText.color = Color.red;
        successText.text = "FAILURE";
    }
}


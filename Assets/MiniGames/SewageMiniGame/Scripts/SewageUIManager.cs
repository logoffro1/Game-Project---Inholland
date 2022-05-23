using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class SewageUIManager : MonoBehaviour // sewage minigame only
{
    public Image[] lifes;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI successText;
    public void ChangeLifes(int lives) // handle lifes sprites
    {
        // show the correct number of lifes
        for (int i = 0; i < lifes.Length; i++)
        {

            if (i < lives)
                lifes[i].enabled = true;
            else
                StartCoroutine(ChangeLives(lifes[i]));
        }
    }
    private IEnumerator ChangeLives(Image life) // play life destroy animation
    {

        Animator animator = life.GetComponent<Animator>();
        animator.SetTrigger("CollectiblePassed");

        yield return new WaitForSeconds(1.5f);
        life.enabled = false;


    }
    public void ChangeScoreText(int score, int maxScore)
    {
        scoreText.text = $"{score} / {maxScore}";
    }
    public void ChangeSuccessText(bool successful)
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

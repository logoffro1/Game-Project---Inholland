using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    [HideInInspector] public TaskObjectType Task;
    public bool isMinigamePopup = true; //else is achievement
    public GameObject design;
    void Start()
    {
        StartCoroutine(WaitAndMove(1f));
    }

    private IEnumerator WaitAndMove(float time)
    {
        //Wait for minigame
        yield return new WaitForSeconds(3f);
        //change image
        //set the text


        //Go down
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (transform.up * -110);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Wait for player to read
        yield return new WaitForSeconds(4f);

        //Disappear //doesn't work
        float alpha = 255f;
        while (alpha > 1)
        {
            foreach(Transform child in design.transform)
            {
                if (child.TryGetComponent(out Image image))
                {
                    Color color = image.color;
                    color.a--;
                    child.GetComponent<Image>().color = color;
                    alpha = color.a;
                }
                else
                {
                    Color color = child.GetComponent<TextMeshProUGUI>().color;
                    color.a--;
                    child.GetComponent<TextMeshProUGUI>().color = color;     
                }
            }
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(this);
    }

}

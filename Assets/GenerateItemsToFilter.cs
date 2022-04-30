using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItemsToFilter : MonoBehaviour
{

    public int AmountOfItems = 10;

    public GameObject[] items;
    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AmountOfItems; i++)
        {
            int rndItem = Random.Range(0, items.Length);

            Instantiate(items[rndItem], RandomPointInBounds(), transform.rotation, transform);
        }
    }

    private Vector3 RandomPointInBounds()
    {
        float boxHeight = box.transform.localScale.y;
        float boxWidth = box.transform.localScale.x;

        Vector3 newPosition = new Vector3(
            Random.Range(boxHeight / -2, boxHeight / 2),
            Random.Range(boxWidth / -2, boxWidth / 2)
            - 0.3f);

        return newPosition + transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject buttonPrefab;
    private SpriteRenderer buttonSprite;
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 3;
    private GameObject[,] buttons;
    private List<GameObject> colorSequence;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new GameObject[width, height];
        buttonSprite = buttonPrefab.GetComponent<SpriteRenderer>();
        colorSequence = new List<GameObject>();

        InitButtons();
        SetSequence();

        StartCoroutine(SetColors());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InitButtons()
    {
        Vector2 spriteSize = buttonSprite.bounds.size;
        float startX = -0.7f;
        float startY = 0.5f;
        float offset = 1.2f;
        for (int i = 0; i < buttons.GetLength(0); i++)
        {
            float spawnX = startX + i * spriteSize.x * offset;
            for (int j = 0; j < buttons.GetLength(1); j++)
            {
                float spawnY = startY - j * spriteSize.y * offset;
                Vector3 spawnPos = new Vector3(spawnX, spawnY, transform.position.z);
                var buttonTemp = Instantiate(buttonPrefab, spawnPos, buttonPrefab.transform.rotation);
                buttons[i, j] = buttonTemp;
            }
        }
    }
    private void SetSequence()
    {

        int sequenceLength = Random.Range(7, 15);
        Debug.Log(sequenceLength);
        for (int i = 0; i < sequenceLength; i++)
        {
            colorSequence.Add(buttons[Random.Range(0, buttons.GetLength(0)), Random.Range(0, buttons.GetLength(1))]);
        }
    }
    private IEnumerator SetColors()
    {

        for (int i = 0; i < colorSequence.Count; i++)
        {
            if (colorSequence[i].TryGetComponent(out SpriteRenderer sprite))
            {
                StartCoroutine(SetColor(sprite, Color.magenta));
                yield return new WaitForSeconds(1f);
            }
        }
    }
    private IEnumerator SetColor(SpriteRenderer sprite, Color color)
    {

        sprite.color = color;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }
}

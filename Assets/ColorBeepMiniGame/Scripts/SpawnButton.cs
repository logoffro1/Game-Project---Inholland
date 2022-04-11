using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

//spaghetti code
//dont take examples from this
public class SpawnButton : MiniGameBase
{
    public GameObject buttonPrefab;
    private SpriteRenderer buttonSprite;
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 3;
    private bool canSelect = false;
    private bool gameOver = false;
    private GameObject[,] buttons;
    private List<GameObject> colorSequence;
    private int selectedCount = 0;
    List<GameObject> selectedSprites = new List<GameObject>();
    public Camera cam;
    public AudioClip playerClick;
    public AudioClip wrongPlayerClick;
    public AudioClip buttonSound;
    public AudioClip winSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new GameObject[width, height];
        buttonSprite = buttonPrefab.GetComponent<SpriteRenderer>();
        colorSequence = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();


        // description = "Repeat the shown sequence!\n\nAfter the sequence is over, click on the buttons in the same order";

        //description = "Herhaal de getoonde volgorde!\n\nNadat de reeks voorbij is, klikt u in dezelfde volgorde op de knoppen";
        SetLocalizedString();

        InitButtons();
        SetSequence();

        StartCoroutine(ShowSequence(1f));
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
                var buttonTemp = Instantiate(buttonPrefab, spawnPos, buttonPrefab.transform.rotation, transform);
                if (buttonTemp.TryGetComponent(out Button btn))
                {
                    btn.onClick += ButtonClicked;
                }
                buttons[i, j] = buttonTemp;
            }
        }
    }

    private void ButtonClicked(GameObject button)
    {
        if (Time.timeScale == 0) return;
        if (!canSelect || gameOver) return;
        Debug.Log(gameOver);
        selectedSprites.Add(button);
        if (!selectedSprites[selectedCount].Equals(colorSequence[selectedCount]))
        {
            audioSource.PlayOneShot(wrongPlayerClick);
            gameOver = true;
            canSelect = false;
            StartCoroutine(ButtonFlash(button, false));

            this.GameOver();
            return;
        }
        selectedCount++;

        if (CheckListMatch()) // win
        {
            canSelect = false;
            audioSource.PlayOneShot(winSound);
            this.GameWon();
        }
        if (canSelect)
            audioSource.PlayOneShot(playerClick);
        StartCoroutine(ButtonFlash(button, true));

    }
    private bool CheckListMatch()
    {
        if (selectedSprites.Count != colorSequence.Count)
            return false;
        for (int i = 0; i < selectedSprites.Count; i++)
        {
            if (selectedSprites[i] != colorSequence[i])
                return false;
        }
        return true;
    }
    private IEnumerator ButtonFlash(GameObject button, bool correctBtn)
    {
        SpriteRenderer sprite = button.GetComponent<SpriteRenderer>();

        if (correctBtn) sprite.color = Color.green;
        else
            sprite.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    private void SetSequence()
    {

        int sequenceLength = Random.Range(4, 6);
        for (int i = 0; i < sequenceLength; i++)
        {
            colorSequence.Add(buttons[Random.Range(0, buttons.GetLength(0)), Random.Range(0, buttons.GetLength(1))]);
        }
    }
    private IEnumerator ShowSequence(float waitTime)
    {
        canSelect = false;
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < colorSequence.Count; i++)
        {
            if (colorSequence[i].TryGetComponent(out SpriteRenderer sprite))
            {
                StartCoroutine(SetColor(sprite, Color.magenta));
                yield return new WaitForSeconds(1f);
            }
        }
        canSelect = true;

    }
    private IEnumerator SetColor(SpriteRenderer sprite, Color color)
    {

        audioSource.PlayOneShot(buttonSound);
        sprite.color = color;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }
}

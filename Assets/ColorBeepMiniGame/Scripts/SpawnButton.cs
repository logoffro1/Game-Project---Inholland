using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spaghetti code
public class SpawnButton : MiniGameBase // whole code for color beep mini game
{
    public GameObject buttonPrefab;
    private SpriteRenderer buttonSprite;
    
    // rows / columns
    [SerializeField] private int width = 4; 
    [SerializeField] private int height = 3;

    private bool canSelect = false;
    private bool gameOver = false;
    private GameObject[,] buttons;
    private List<GameObject> colorSequence;
    private int selectedCount = 0;

    List<GameObject> selectedSprites = new List<GameObject>(); // current input pattern
    public Camera cam;
    public AudioClip playerClick;
    public AudioClip wrongPlayerClick;
    public AudioClip buttonSound;
    public AudioClip winSound;
    private AudioSource audioSource;

    private float sequenceWaitTime = 1f; // wait time between showing new button in sequence
    void Start()
    {
        StartCoroutine(WaitBeforeStarting(WaitTime));
    }

    private IEnumerator WaitBeforeStarting(float time)// initialize buttons and prepare sequence
    {
        buttons = new GameObject[width, height];
        buttonSprite = buttonPrefab.GetComponent<SpriteRenderer>();
        colorSequence = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
        SetLocalizedString();

        InitButtons();

        yield return new WaitForSeconds(time);

        SetSequence();

        StartCoroutine(ShowSequence(sequenceWaitTime));

    }

    private void InitButtons()
    {
        Vector2 spriteSize = buttonSprite.bounds.size;

        //start pos
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
                var buttonTemp = Instantiate(buttonPrefab, spawnPos, buttonPrefab.transform.rotation, transform); // spawned button
                if (buttonTemp.TryGetComponent(out Button btn))
                {
                    btn.onClick += ButtonClicked; // subscribe to clicking event
                }
                buttons[i, j] = buttonTemp; // set button in 2D array
            }
        }
    }

    private void ButtonClicked(GameObject button)
    {
        // make sure the player is allowed to click
        if (Time.timeScale == 0) return;
        if (!canSelect || gameOver) return;
        selectedSprites.Add(button);
        //check if button clicked is in the correct sequence
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
        // check if the color sequence and the player input sequence are the same

        if (selectedSprites.Count != colorSequence.Count)
            return false;
        for (int i = 0; i < selectedSprites.Count; i++)
        {
            if (selectedSprites[i] != colorSequence[i])
                return false;
        }
        return true;
    }
    private IEnumerator ButtonFlash(GameObject button, bool correctBtn) // change button color, press button
    {
        GameObject buttonUnpressed = button.transform.Find("ButtonUnpressed").gameObject;
        GameObject buttonPressed = button.transform.Find("ButtonPressed").gameObject;
        SpriteRenderer sprite = buttonPressed.GetComponent<SpriteRenderer>();

        // set button color
        if (correctBtn) sprite.color = Color.green;
        else
            sprite.color = Color.red;

        buttonPressed.SetActive(true);
        buttonUnpressed.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        // unpress button
        sprite.color = Color.white;
        buttonPressed.SetActive(false);
        buttonUnpressed.SetActive(true);
    }
    private void SetSequence() // set the initial collor sequence
    {

        int sequenceLength = Random.Range(4, 6);
        for (int i = 0; i < sequenceLength; i++)
        {
            colorSequence.Add(buttons[Random.Range(0, buttons.GetLength(0)), Random.Range(0, buttons.GetLength(1))]);
        }
    }
    private IEnumerator ShowSequence(float waitTime) // show the initial color sequence
    {
        canSelect = false;
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < colorSequence.Count; i++)
        {
            // press buttons
            GameObject buttonUnpressed = colorSequence[i].transform.Find("ButtonUnpressed").gameObject;
            GameObject buttonPressed = colorSequence[i].transform.Find("ButtonPressed").gameObject;
            if(buttonPressed != null && buttonUnpressed != null)
            {
                StartCoroutine(SequencePressButton(buttonUnpressed,buttonPressed, Color.magenta));
                yield return new WaitForSeconds(sequenceWaitTime);
            }


        }
        canSelect = true;
    }
    public override void CoordinateLevel() // change wait time based on difficulty
    {
        if (this.Level <= 40f) sequenceWaitTime = 1f;
        else if (this.Level <= 65f) sequenceWaitTime = 0.7f;
        else sequenceWaitTime = 0.45f;
    }
    private IEnumerator SequencePressButton(GameObject buttonUnpressed, GameObject buttonPressed, Color color)
    {
        // press and unpress button
        audioSource.PlayOneShot(buttonSound);
        buttonUnpressed.SetActive(false);
        buttonPressed.SetActive(true);
        if(buttonPressed.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.color = color;
        }
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
        buttonUnpressed.SetActive(true);
        buttonPressed.SetActive(false);
    }
}

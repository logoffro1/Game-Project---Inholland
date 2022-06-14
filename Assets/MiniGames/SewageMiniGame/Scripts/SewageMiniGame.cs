using UnityEngine;

public class SewageMiniGame : MiniGameBase
{
    private int lives = 3;
    private int score = 0;
    private int maxScore = 10;
    private static SewageMiniGame _instance = null;
    public static SewageMiniGame Instance { get { return _instance; } }
    private SewageUIManager sewageUIManager;
    private void Awake() // singleton
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    private void Start()
    {
        sewageUIManager = GameObject.FindObjectOfType<SewageUIManager>();
        DetermineGameDifficulty();
        SetLocalizedString();
    }

    public override void DetermineGameDifficulty()
    {
        this.gameDifficulty = MiniGameDifficulty.Hard;
    }
    public void DecreaseLives()
    {
        lives -= 1;
        if (lives <= 0)
        {
            lives = 0;
            this.GameOver();
        }

        sewageUIManager.ChangeLifes(lives);
    }
    public void IncreaseScore()
    {
        score += 1;

        if (score >= maxScore)
            this.GameWon();

        sewageUIManager.ChangeScoreText(score, maxScore);
    }
    public override void CoordinateLevel() // change speed based on game difficulty
    {
        float newSpeed = this.Level / 150;
        FindObjectOfType<SpawnManager>().SetCorridorSpeed(newSpeed);
    }
}

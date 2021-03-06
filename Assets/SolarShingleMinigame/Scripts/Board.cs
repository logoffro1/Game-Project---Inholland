using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    //This class controls tetris mini game.
    public Tilemap tilemap { get; private set; }

    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(7, 8);
    public AudioSource audioSource;
    public AudioClip lineClear;
    public TextMeshProUGUI amountText;
    public Image solarPanelImage;
    public int imageFillMaxValue;

    public ShinglesMiniGame shinglesGame;
    public int amountOfLinesNeeded { get;  set; }

    public bool isGameOver = false;
    //Declaring the bounds and the board size.
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.solarPanelImage.fillAmount = 0f;
        this.audioSource = GetComponent<AudioSource>();
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for (int i = 0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Init();
        }


    }
    //This method spawns a piece from the middle top of the board and checks the win condition.
    public void SpawnPiece()

    {
        if (isGameOver) return;
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];
        this.activePiece.Init(this, spawnPosition, data);

        if (!IsValidPosition(this.activePiece, this.spawnPosition))
        {
            isGameOver = true;
            shinglesGame.GameFinish(false);
            amountText.text = "0";
        }
        else
        {
            SetPiece(this.activePiece);
        }


    }
    private void GameOver()
    {
        this.tilemap.ClearAllTiles();
    }

    //This method simply spawns the tetris piece.
    public void SetPiece(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    //This method clears a certain tile.
    public void ClearTile(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }
    //Checks the validity of the position via the tetromino data and the bounds declared.
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for (int i = 0; i < piece.cells.Length; i++)
        {

            Vector3Int tilePosition = piece.cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }
    //Cleans the line once there is a perfect line.
    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        while (row < bounds.yMax)
        {
            if (isLineFull(row))
            {
                LineClear(row);
                this.amountOfLinesNeeded--;
                this.amountText.text = $"{amountOfLinesNeeded}";
                float diff = (float)imageFillMaxValue - (float)amountOfLinesNeeded;
                Debug.Log(diff);
                this.solarPanelImage.fillAmount = diff/ (float)imageFillMaxValue;
                if (amountOfLinesNeeded <= 0)
                {
                    this.amountText.text = "0";
                    isGameOver = true;
                    shinglesGame.GameFinish(true);
                }
                else
                {
                    this.audioSource.PlayOneShot(lineClear);
                }
            }
            else
            {
                row++;

            }

        }
    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                //Row is +1 because we have to grab the tile above the cleared lines.
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase aboveTile = this.tilemap.GetTile(position);
                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, aboveTile);
            }
            row++;
        }
    }
    //Checking if the line is full, if there is a single tile with no tetris piece, return false.
    private bool isLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }
    // Begin by spawning the first piece that creates the cycle.
    void Start()
    {
        SpawnPiece();
    }

}

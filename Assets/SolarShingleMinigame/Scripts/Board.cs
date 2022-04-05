using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }

    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(7,8);

    public ShinglesMiniGame shinglesGame;
    public int amountOfLinesNeeded { get; private set; }

    public bool isGameOver = false;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2 ,-this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        this.amountOfLinesNeeded = 3; //Basic difficulty.
        for(int i =0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Init();
        }


    }
    public void SpawnPiece()
    {        
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];
        this.activePiece.Init(this, spawnPosition, data);

        if (!IsValidPosition(this.activePiece, this.spawnPosition))
        {
            isGameOver = true;
            shinglesGame.GameFinish(false);
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

    public void SetPiece(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    public void ClearTile(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for(int i = 0; i < piece.cells.Length; i++)
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

    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
       while(row < bounds.yMax)
        {
            if(isLineFull(row))
            {
                LineClear(row);
                this.amountOfLinesNeeded--;
                if(amountOfLinesNeeded <= 0)
                {
                    isGameOver = true;
                    shinglesGame.GameFinish(true);
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

        while(row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                //Row is +1 because we have to grab the tile above the cleared lines.
                Vector3Int position = new Vector3Int(col, row+1, 0);
                TileBase aboveTile = this.tilemap.GetTile(position);
                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, aboveTile);
            }
            row++;
        }
    }

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
    void Start()
    {
        SpawnPiece();
    }
   
}

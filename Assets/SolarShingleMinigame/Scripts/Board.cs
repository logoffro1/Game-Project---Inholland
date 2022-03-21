using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public TetrominoData[] tetrominoes;
    public Tilemap tilemap { get; private set; }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        for(int i =0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Init();
        }
    }
    void Start()
    {
        SpawnPiece();
    }
    void Update()
    {

    }
    public void SpawnPiece()
    {
        int random = Random.Range(0,tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];
    }

    public void SetTiles()
    {

    }

    // Update is called once per frame
   
}

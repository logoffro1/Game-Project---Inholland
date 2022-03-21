using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData tetrisData { get; private set; }
    public Vector3Int position { get; private set; }

    public Vector2Int[] cells { get; private set; }
    public void Init(Board board,Vector3Int position, TetrominoData tetrisData)
    {
        this.board = board;
        this.position = position;
        this.tetrisData = tetrisData;
    }
}

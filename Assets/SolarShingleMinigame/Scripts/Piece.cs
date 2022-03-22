using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData tetrisData { get; private set; }
    public Vector3Int position { get; private set; }

    public Vector3Int[] cells { get; private set; }
    public void Init(Board board,Vector3Int position, TetrominoData tetrisData)
    {
        this.board = board;
        this.position = position;
        this.tetrisData = tetrisData;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[tetrisData.cells.Length];
        }

        for(int i = 0; i< tetrisData.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)tetrisData.cells[i];
        }
    }
    private void Update()
    {
        this.board.ClearTile(this);
        if (Input.GetKeyDown(KeyCode.J))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Move(Vector2Int.right);
        }

        if (Input.GetKeyDown(KeyCode.K)){
            Move(Vector2Int.down);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            HardDrop();
        }

        this.board.SetPiece(this);
    }
    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this,newPosition);

        if (valid)
        {
            this.position = newPosition;
        }
        print("is it valid? " + valid);
        return valid;
        
    }
}

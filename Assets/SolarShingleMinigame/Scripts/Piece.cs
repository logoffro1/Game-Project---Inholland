using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int position { get; private set; }

    public Vector3Int[] cells { get; private set; }

    public int rotationIndex { get; private set; }


    public float stepDelay = 1f; //By default its one second.

    public float lockDelay = 0.5f; // These fields can be customizable for increasing difficulty.

    private float stepTime;
    private float lockTime;
    public void Init(Board board,Vector3Int position, TetrominoData tetrisData)
    {
        this.board = board;
        this.position = position;
        this.data = tetrisData;
        rotationIndex = 0;
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;

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
        this.lockTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }

        if (Input.GetKeyDown(KeyCode.S)){
            Move(Vector2Int.down);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            HardDrop();
        }
        if (Time.time >= this.stepTime)
        {
            Step();
        }

        this.board.SetPiece(this);
    }
    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;

        Move(Vector2Int.down);

        if(this.lockTime >= this.lockDelay)
        {
            Lock();
        }
    }
    private void Lock()
    {
        this.board.SetPiece(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
    }
    private void Rotate(int direction)
    {
        //We wrap between 0 and 4 because thats the limit of potential rotations.
        int originalIndex = this.rotationIndex;
        this.rotationIndex = Wrap(direction, 0, 4);

        ApplyRotationMatrix(direction);
        if (!TestWallKicks(this.rotationIndex,direction))
        {
            //if wallkicks method fails, revert the rotation since it should not work.
            this.rotationIndex = originalIndex;
            ApplyRotationMatrix(-direction);
        }
        
    }

    void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];
            int x, y;
            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    // "I" and "O" are rotated from an offset center point
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int indexRotation,  int directionRotation)
    {
        int wallKickIndex = GetWallKicks(indexRotation, directionRotation);

        for (int i=0; i < this.data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];
            if (Move(translation))
            {
                return true;
            }
        }
        return false;
    }
    private int GetWallKicks(int indexRotation, int directionRotation)
    {
        int wallKickIndex = indexRotation * 2;

        if (directionRotation < 0)
        {
            wallKickIndex--;
        }
        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }

    private void HardDrop()
    {   
        while (Move(Vector2Int.down))
        {
            continue;
        }
        Lock();
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
            this.lockTime = 0f;
        }
        return valid;
        
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}

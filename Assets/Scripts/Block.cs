using UnityEngine;
using UnityEngine.Tilemaps;

public enum rotationDirection
{
    Clockwise,
    CounterClockwise,
}

public class Block : MonoBehaviour
{
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int pos { get; private set; }
    public int rotationIndex { get; private set; }

    // tetromino 구조 데이터와 블럭의 위치을 초기값으로 받음
    public void Initialize(TetrominoData _data, Vector3Int _pos)
    {
        data = _data;
        pos = _pos;
        rotationIndex = 0;

        if (cells == null) 
        {
            cells = new Vector3Int[_data.cells.Length]; 
        }

        for (int i = 0; i < _data.cells.Length; ++i) 
        {
            cells[i] = (Vector3Int) _data.cells[i];
        }
    }

    public void Move(Vector3Int _pos)
    {
        pos = _pos;
    }

    // makes block fall
    public void Fall()
    {
        pos += Vector3Int.down;
    }

    // makes block rise
    public void Rise()
    {
        pos += Vector3Int.up;
    }

    // makes block go right
    public void GoRight()
    {
        pos += Vector3Int.right;
    }

    // makes block go left
    public void GoLeft()
    {
        pos += Vector3Int.left;
    }

    public void Rotate()
    {
        for (int i = 0; i < cells.Length; ++i)
        {
            Vector3 cell = cells[i];

            int x;
            int y;

            switch (data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt(BlockData.RotationMatrix[0] * cell.x + BlockData.RotationMatrix[1] * cell.y);
                    y = Mathf.CeilToInt(BlockData.RotationMatrix[2] * cell.x + BlockData.RotationMatrix[3] * cell.y);
                    break;
                default:
                    x = Mathf.RoundToInt(BlockData.RotationMatrix[0] * cell.x + BlockData.RotationMatrix[1] * cell.y);
                    y = Mathf.RoundToInt(BlockData.RotationMatrix[2] * cell.x + BlockData.RotationMatrix[3] * cell.y);
                    break;
            }

            cells[i] = new Vector3Int(x, y, 0);
        }

        if (rotationIndex >= 3)
        {
            rotationIndex -= 3;
        }
        else
        {
            rotationIndex++;
        }
    }

    public void RotateBack()
    {
        for (int i = 0; i < cells.Length; ++i)
        {
            Vector3 cell = cells[i];

            int x;
            int y;

            switch (data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt(BlockData.RotationMatrix[0] * cell.x + BlockData.RotationMatrix[2] * cell.y);
                    y = Mathf.CeilToInt(BlockData.RotationMatrix[1] * cell.x + BlockData.RotationMatrix[3] * cell.y);
                    break;
                default:
                    x = Mathf.RoundToInt(BlockData.RotationMatrix[0] * cell.x + BlockData.RotationMatrix[2] * cell.y);
                    y = Mathf.RoundToInt(BlockData.RotationMatrix[1] * cell.x + BlockData.RotationMatrix[3] * cell.y);
                    break;
            }

            cells[i] = new Vector3Int(x, y, 0);
        }

        if (rotationIndex <= 0)
        {
            rotationIndex += 3;
        }
        else
        {
            rotationIndex--;
        }
    }

    public int GetWallKickIndex(int _rotationIndex, rotationDirection _rotationDirection)
    {
        int wallKickIndex = _rotationIndex * 2;

        if (_rotationDirection == rotationDirection.CounterClockwise)
        {
            wallKickIndex++;
        }

        return wallKickIndex;
    }
}
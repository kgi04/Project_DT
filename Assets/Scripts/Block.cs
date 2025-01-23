using UnityEngine;
using UnityEngine.Tilemaps;

public class Block : MonoBehaviour
{
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int pos { get; private set; }

    // tetromino 구조 데이터와 블럭의 위치을 초기값으로 받음
    public void Initialize(TetrominoData _data, Vector3Int _pos)
    {
        data = _data;
        pos = _pos;

        if (cells == null) 
        {
            cells = new Vector3Int[_data.cells.Length]; 
        }

        for (int i = 0; i < _data.cells.Length; ++i) 
        {
            cells[i] = (Vector3Int) _data.cells[i];
        }
    }
}
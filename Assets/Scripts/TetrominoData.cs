using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Tilemaps;

// tetris에 사용될 블럭 종류
public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
    nullTetromino,
}

// tetris에 사용될 블럭의 데이터
[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wallkicks { get; private set; }

    public void Initialize()
    {
        cells = BlockData.Cells[tetromino];
        wallkicks = BlockData.WallKicks[tetromino];
    }
}
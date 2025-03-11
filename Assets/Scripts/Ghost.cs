using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : Block
{
    public Tile GhostTile;

    public override void Initialize(TetrominoData _data, Vector3Int _pos)
    {
        data = _data;
        pos = _pos;

        _data.tile = GhostTile;

        if (cells == null)
        {
            cells = new Vector3Int[_data.cells.Length];
        }

        for (int i = 0; i < _data.cells.Length; ++i)
        {
            cells[i] = (Vector3Int)_data.cells[i];
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

// fuck
public class Ghost : MonoBehaviour
{
    public Board Board;
    public Tile GhostTile;
    public Block TrackingBlock;
    public Tilemap GhostTilemap;

    public Vector3Int[] cells { get; private set; }
    public Vector3Int pos { get; private set; }

    private void Awake()
    {
        cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        DeleteBlock();
        Copy();
        Drop();
        Render();
    }

    private void DeleteBlock()
    {
        GhostTilemap.ClearAllTiles();
    }

    private void Copy()
    {
        for (int i = 0; i < TrackingBlock.cells.Length; ++i)
        {
            cells[i] = TrackingBlock.cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int position = TrackingBlock.pos;

        int current_y = position.y;
        int groundYpos = -10;

        for (int i = current_y; i >= groundYpos; --i)
        {
            position.y = i;

            if (Board.CanMove(TrackingBlock, Vector3Int.down))
            {
                pos = position;
            }
            else
            {
                break;
            }
        }
    }

    private void Render()
    {
        for (int i = 0; i < cells.Length; ++i)
        {
            GhostTilemap.SetTile(pos + cells[i], GhostTile);
        }
    }
}

using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tile GhostTile;
    public Tilemap Tilemap;
    public Block trackingBlock;
    public Tilemap trackingTilemap;

    public Vector3Int[] gcell { get; private set; }
    public Vector3Int gpos { get; private set; }

    int groundYpos = -10;

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Render();
    }

    // clears the ghost tilemap to make it ready for the next frame
    private void Clear()
    {
        Tilemap.ClearAllTiles();
    }

    // copies the tracking block's cells and position
    private void Copy()
    {
        gcell = trackingBlock.cells;
        gpos = trackingBlock.pos;
    }

    // drops the ghost block to the ground
    private void Drop()
    {
        while (CanDrop())
        {
            gpos += Vector3Int.down;
        }
    }

    // renders the ghost block
    private void Render()
    {
        for (int i = 0; i < gcell.Length; ++i)
        {
            Tilemap.SetTile(gpos + gcell[i], GhostTile);
        }
    }

    // checks if the tiles under the ghost block is empty
    private bool CanDrop()
    {
        for (int i = 0; i < gcell.Length; ++i)
        {
            Vector3Int cell = gcell[i];
            Vector3Int next = gpos + cell + Vector3Int.down;
            if (trackingTilemap.HasTile(next) || next.y < groundYpos)
            {
                return false;
            }
        }
        return true;
    }
}

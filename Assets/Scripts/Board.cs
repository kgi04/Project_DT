using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap;
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPos;
    public Block activeBlock { get; private set; }

    private void Awake()
    {
        activeBlock = GetComponent<Block>();

        for (int i = 0; i < tetrominoes.Length; ++i) { tetrominoes[i].Initialize(); }
    }

    private void Start()
    {
        SpawnBlock();
    }

    // ������ ���� ����
    public void SpawnBlock()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData tetrisBlock = tetrominoes[random];

        activeBlock.Initialize(tetrisBlock, spawnPos);
        Render(activeBlock);
    }

    // ���� board�� ������
    public void Render(Block _block)
    {
        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int tilePos = _block.cells[i] + _block.pos;
            tilemap.SetTile(tilePos, _block.data.tile);
        }
    }
}

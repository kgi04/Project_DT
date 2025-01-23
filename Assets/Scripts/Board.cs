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

    // 랜덤한 블럭을 생성
    public void SpawnBlock()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData tetrisBlock = tetrominoes[random];

        activeBlock.Initialize(tetrisBlock, spawnPos);
        Render(activeBlock);
    }

    // 블럭을 board에 렌더링
    public void Render(Block _block)
    {
        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int tilePos = _block.cells[i] + _block.pos;
            tilemap.SetTile(tilePos, _block.data.tile);
        }
    }
}

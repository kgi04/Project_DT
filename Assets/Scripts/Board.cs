using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap;
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPos;
    public Block activeBlock { get; private set; }

    float pointTime = 1.0f;
    float nextTime = 0.0f;

    int groundYpos = -10;

    private void Awake()
    {
        activeBlock = GetComponent<Block>();

        for (int i = 0; i < tetrominoes.Length; ++i) { tetrominoes[i].Initialize(); }
    }

    private void Start()
    {
        SpawnBlock();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Return))
        {
            activeBlock.Fall();

            Render(activeBlock);
        }

        if (activeBlock.pos.y < groundYpos)
        {
            activeBlock.Deactivate();

            SpawnBlock();
        }

        if (Time.time > nextTime)
        {
            nextTime = Time.time + pointTime;

            activeBlock.Fall();
            Render(activeBlock);
        }
    }

    // 랜덤한 블럭을 생성
    public void SpawnBlock()
    {
        int random = UnityEngine.Random.Range(0, tetrominoes.Length);
        TetrominoData tetrisBlock = tetrominoes[random];

        activeBlock.Initialize(tetrisBlock, spawnPos);
        Render(activeBlock);
    }

    // 블럭을 board에 렌더링
    public void Render(Block _block)
    {
        tilemap.ClearAllTiles();

        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int tilePos = _block.cells[i] + _block.pos;
            tilemap.SetTile(tilePos, _block.data.tile);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.Collections.AllocatorManager;

public class Board : MonoBehaviour
{
    public Tilemap InactiveTilemap;
    public Tilemap ActiveTilemap;
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPos;
    public Block activeBlock { get; private set; }

    float pointTime = 1.0f;
    float nextTime = 0.0f;
    float moveintervalTime = 0.1f;
    float movetimer = 0.0f;

    int groundYpos = -10;
    int leftEnd = -5;
    int rightEnd = 4;

    bool IsMoveActive = true;

    private void Awake()
    {
        activeBlock = GetComponent<Block>();

        for (int i = 0; i < tetrominoes.Length; ++i) 
        {
            tetrominoes[i].Initialize(); 
        }
    }

    private void Start()
    {
        SpawnBlock();
    }

    private void Update()
    {
        CompleteRow();

        if (!IsMoveActive)
        {
            movetimer += Time.deltaTime;

            if (movetimer > moveintervalTime)
            {
                IsMoveActive = true;
                movetimer = 0.0f;
            }
        }
        else
        {
            KeyMove();
            IsMoveActive = false;
        }

        BlockRotation();

        if (Time.time > nextTime)
        {
            nextTime = Time.time + pointTime;

            DeleteBlock(activeBlock);
            if (CanMove(activeBlock, Vector3Int.down))
            {
                activeBlock.Fall();
            }
            Render(activeBlock);
        }

        for (int i = 0; i < activeBlock.cells.Length; ++i)
        {
            Vector3Int newPos = activeBlock.cells[i] + activeBlock.pos + Vector3Int.down;
            if (newPos.y < groundYpos || InactiveTilemap.GetTile(newPos) != null)
            {
                DeleteBlock(activeBlock);
                RenderInactive(activeBlock);
                SpawnBlock();
            }
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

    // 블럭을 board에 렌더링 (in inactivetilemap)
    public void Render(Block _block)
    {
        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int tilePos = _block.cells[i] + _block.pos;
            ActiveTilemap.SetTile(tilePos, _block.data.tile);
        }
    }

    public void RenderInactive(Block _block)
    {
        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int tilePos = _block.cells[i] + _block.pos;
            InactiveTilemap.SetTile(tilePos, _block.data.tile);
        }
    }

    // delete tiles in blockpos
    public void DeleteBlock(Block _block)
    {
        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int tilePos = _block.cells[i] + _block.pos;
            ActiveTilemap.SetTile(tilePos, null);
        }
    }

    // check if block can move to some direction
    private bool CanMove(Block _block, Vector3Int _direction)
    {
        for (int i = 0; i < _block.cells.Length; ++i)
        {
            Vector3Int newPos = _block.cells[i] + _block.pos + _direction;

            // Out of board bounds?
            if (newPos.x < leftEnd || newPos.x > rightEnd || newPos.y < groundYpos)
            {
                return false;
            }

            // Collision with another tile?
            if (InactiveTilemap.GetTile(newPos) != null)
            {
                return false;
            }
        }
        return true;
    }

    // check if block can rotate
    private bool CanRot(Block _block)
    {
        _block.Rotate();

        for (int i = 0; i < _block.cells.Length; ++i)
        {            
            Vector3Int newPos = _block.cells[i] + _block.pos;

            // Out of board bounds?
            if (newPos.x < leftEnd || newPos.x > rightEnd || newPos.y < groundYpos)
            {
                _block.RotateBack();
                return false;
            }

            // Collision with another tile?
            if (InactiveTilemap.GetTile(newPos) != null)
            {
                _block.RotateBack();
                return false;
            }
        }
        _block.RotateBack();
        return true;
    }

    // check if some rows are full, and then clear the row && drop the rows above that row
    public void CompleteRow()
    {
        for (int y = groundYpos; y <= spawnPos.y + 1; ++y)
        {
            if (IsRowFull(y))
            {
                ClearRow(y);
                DropRowAbove(y);
                y--;
            }
        }
    }

    private bool IsRowFull(int _y)
    {
        for (int x = leftEnd; x <= rightEnd; ++x)
        {
            Vector3Int pos = new Vector3Int(x, _y, 0);
            if (InactiveTilemap.GetTile(pos) == null)
            {
                return false;
            }
        }
        return true;
    }

    private void ClearRow(int _y)
    {
        for (int x = leftEnd; x <= rightEnd; ++x)
        {
            Vector3Int pos = new Vector3Int(x, _y, 0);
            InactiveTilemap.SetTile(pos, null);
        }
    }

    private void DropRowAbove(int _y)
    {
        for (int y = _y + 1; y <= spawnPos.y + 1; ++y)
        {
            for (int x = leftEnd; x <= rightEnd; ++x)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                if (InactiveTilemap.GetTile(pos) != null)
                {
                    Vector3Int newPos = new Vector3Int(x, y - 1, 0);

                    InactiveTilemap.SetTile(newPos, InactiveTilemap.GetTile(pos));
                    InactiveTilemap.SetTile(pos, null);
                }
            }
        }
    }

    private void KeyMove()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Return))
        {
            DeleteBlock(activeBlock);
            if (CanMove(activeBlock, Vector3Int.down))
            {
                activeBlock.Fall();
            }
            Render(activeBlock);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            DeleteBlock(activeBlock);
            if (CanMove(activeBlock, Vector3Int.right))
            {
                activeBlock.GoRight();
            }
            Render(activeBlock);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            DeleteBlock(activeBlock);
            if (CanMove(activeBlock, Vector3Int.left))
            {
                activeBlock.GoLeft();
            }
            Render(activeBlock);
        }
    }

    // makes block rotate
    // 아직 I블럭은 회전이 제대로 되지 않음
    private void BlockRotation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DeleteBlock(activeBlock);
            if (CanRot(activeBlock))
            {
                activeBlock.Rotate();
            }
            else
            {
                if (CanMove(activeBlock, Vector3Int.left))
                {
                    activeBlock.GoLeft();
                    if (CanRot(activeBlock))
                    {
                        activeBlock.Rotate();
                    }
                    else
                    {
                        activeBlock.GoRight();
                    }
                }
                else if (CanMove(activeBlock, Vector3Int.right))
                {
                    activeBlock.GoRight();
                    if (CanRot(activeBlock))
                    {
                        activeBlock.Rotate();
                    }
                    else
                    {
                        activeBlock.GoLeft();
                    }
                }
                else if (CanMove(activeBlock, Vector3Int.up))
                {
                    activeBlock.Rise();
                    if (CanRot(activeBlock))
                    {
                        activeBlock.Rotate();
                    }
                    else
                    {
                        activeBlock.Fall();
                    }
                }
            }
            Render(activeBlock);
        }
    }
}
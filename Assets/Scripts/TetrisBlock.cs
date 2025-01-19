using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public List<int> gridSize = new List<int>();
    public Transform[] blockPieces;
    public float TimeInterval = 1f;
    public float unitlength = 0.4f;
    float timer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBlock(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveBlock(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBlock(Vector3.down);
        }

        // TimeInterval마다 블럭이 1칸씩 움직임
        timer += Time.deltaTime;
        if (timer >= TimeInterval)
        {
            MoveBlock(Vector3.down);
            timer = 0f;
        }
    }

    void MoveBlock(Vector3 direction)
    {
        transform.position += direction * unitlength;

        // 이동 후 충돌 체크
        if (!IsPositionValid())
        {
            transform.position -= direction * unitlength;
        }
    }

    public virtual bool IsPositionValid()
    {
        // 각 block의 모양에 맞게 재정의 필요
        foreach (Transform piece in blockPieces)
        {
            Vector2Int pos = Vector2Int.RoundToInt(piece.localPosition * 2.5f);

            if (pos.x < 0 || pos.x >= gridSize[0] || pos.y < 0 || pos.y >= gridSize[1])
            {
                return false;
            }

            if (IsOccupied(pos))
            {
                return false;
            }

            if (pos.y <= 0)
            {
                this.enabled = false;
            }
        }
        return true;
    }

    bool IsOccupied(Vector2Int position)
    {
        // 이 함수는 현재 그리드의 상태를 확인하고
        // 해당 위치에 블록이 이미 차있으면 true를 반환하도록 구현
        return false;
    }
}

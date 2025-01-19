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

        // TimeInterval���� ���� 1ĭ�� ������
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

        // �̵� �� �浹 üũ
        if (!IsPositionValid())
        {
            transform.position -= direction * unitlength;
        }
    }

    public virtual bool IsPositionValid()
    {
        // �� block�� ��翡 �°� ������ �ʿ�
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
        // �� �Լ��� ���� �׸����� ���¸� Ȯ���ϰ�
        // �ش� ��ġ�� ����� �̹� �������� true�� ��ȯ�ϵ��� ����
        return false;
    }
}

using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    private void Awake()
    {
        score = 0;
    }
}

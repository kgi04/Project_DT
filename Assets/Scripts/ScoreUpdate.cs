using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + ScoreManager.score.ToString();
    }
}

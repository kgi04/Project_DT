using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public bool IsGamePaused = false;

    public Button PauseBtn;
    public GameObject ResumeBtn;
    public GameObject OptionBtn;
    public GameObject QuitBtn;

    private void Awake()
    {
        // scene을 전환할 때 기존의 객체를 파괴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // scene이 로드될 때마다 이벤트를 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // scene이 로드될 때마다 호출되어 score를 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            score = 0;
            IsGamePaused = false;
        }
    }

    // scene 로드 시 이벤트 해제
    private void OnDestoy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (IsGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        PauseBtn.interactable = true;
        ResumeBtn.SetActive(false);
        OptionBtn.SetActive(false);
        QuitBtn.SetActive(false);
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        PauseBtn.interactable = false;
        ResumeBtn.SetActive(true);
        OptionBtn.SetActive(true);
        QuitBtn.SetActive(true);
    }
}

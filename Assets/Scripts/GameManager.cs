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
        // scene�� ��ȯ�� �� ������ ��ü�� �ı�
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // scene�� �ε�� ������ �̺�Ʈ�� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // scene�� �ε�� ������ ȣ��Ǿ� score�� �ʱ�ȭ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            score = 0;
            IsGamePaused = false;
        }
    }

    // scene �ε� �� �̺�Ʈ ����
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

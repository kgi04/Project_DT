using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "StartButton":
                SceneManager.LoadScene("GameScene");
                break;
            case "LoadButton":
                Debug.Log("Load");
                break;
            case "OptionButton":
                Debug.Log("Option");
                break;
            case "QuitButton":
                UnityEditor.EditorApplication.isPlaying = false;
                break;
        }
    }
}

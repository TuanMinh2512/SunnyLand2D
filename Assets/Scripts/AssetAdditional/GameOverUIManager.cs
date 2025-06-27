using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Nut bat dau choi
    public void StartGame()
    {
        PermanentUI.perm?.FullReset();
        SceneManager.LoadScene("Level1");
    }

    // Nut OPTIONS
    public void OpenOptions()
    {
        SceneManager.LoadScene("Options");
    }

    // Nut LEADERBOARD
    public void OpenLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Nut GUIDE
    public void OpenGuide()
    {
        SceneManager.LoadScene("Guide");
    }

    // Nut EXIT
    public void ExitGame()
    {
        Application.Quit(); // Se thoat khi build thanh .exe
        Debug.Log("Thoat game");
    }
}

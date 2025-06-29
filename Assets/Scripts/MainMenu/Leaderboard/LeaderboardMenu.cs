using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardMenu : MonoBehaviour
{
    public void OpenLevel1Leaderboard()
    {
        PlayerPrefs.SetInt("LevelToShow", 1);
        SceneManager.LoadScene("Leaderboard_Show");
    }

    public void OpenLevel2Leaderboard()
    {
        PlayerPrefs.SetInt("LevelToShow", 2);
        SceneManager.LoadScene("Leaderboard_Show");
    }

    public void OpenLevel3Leaderboard()
    {
        PlayerPrefs.SetInt("LevelToShow", 3);
        SceneManager.LoadScene("Leaderboard_Show");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

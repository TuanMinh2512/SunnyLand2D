using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardShow : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        int level = PlayerPrefs.GetInt("LevelToShow", 0);
        levelText.text = level.ToString(); // chi hien thi so
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

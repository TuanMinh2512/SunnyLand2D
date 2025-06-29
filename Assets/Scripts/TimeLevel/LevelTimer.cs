using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private LevelSettingsDatabase settingsDatabase;
    [SerializeField]private TextMeshProUGUI timerText;

    private float timeLeft;
    private bool levelCompleted = false;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (levelCompleted) return;

        timeLeft -= Time.deltaTime; //cái này do m?i máy load khác nhau, nên g?i nh? này thì nó tr? ?úng v?i time th?c
        if (timerText)
        {
            timerText.text = $"Time: {Mathf.Max(0, timeLeft):F1}s";
        }
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
    }

    public void ResetTimer()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        float time = settingsDatabase.GetTimeLimitForScene(currentScene);
        Debug.Log($"[ResetTimer] Scene: {currentScene}, time reset to: {time}");

        timeLeft = time;
        levelCompleted = false;

        if (timerText != null)
        {
            timerText.text = $"Time: {timeLeft:F1}s";
        }
    }

    public float GetTimeComplete()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        float totalTime = settingsDatabase.GetTimeLimitForScene(currentScene);
        return totalTime - timeLeft;
    }
}

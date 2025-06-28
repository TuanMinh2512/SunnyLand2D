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
        PermanentUI.perm?.UpdateUI();
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
        timeLeft = settingsDatabase.GetTimeLimitForScene(currentScene);
        levelCompleted = false;

        if (timerText != null)
        {
            timerText.text = $"Time: {timeLeft:F1}s";
        }
    }

}

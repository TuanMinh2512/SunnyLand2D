using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 60f;
    private float timeLeft;
    public TextMeshProUGUI timerText;
    private bool levelCompleted = false;

    private void Start()
    {
        timeLeft = timeLimit;
    }

    private void Update()
    {
        if (levelCompleted) return;

        timeLeft -= Time.deltaTime;
        if (timerText) timerText.text = $"Level Time Left: {Mathf.Max(0, timeLeft):F1}s";

        if (timeLeft <= 0)
        {
            PermanentUI.perm.FullReset();
            SceneManager.LoadScene("Level1");
        }
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
    }
}

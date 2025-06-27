using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]private float timeLimit = 60f;
    [SerializeField]private TextMeshProUGUI timerText;

    private float timeLeft;
    private bool levelCompleted = false;

    private void Start()
    {
        timeLeft = timeLimit;
    }

    private void Update()
    {
        if (levelCompleted) return;

        timeLeft -= Time.deltaTime; //c�i n�y do m?i m�y load kh�c nhau, n�n g?i nh? n�y th� n� tr? ?�ng v?i time th?c
        if (timerText)
        {
            timerText.text = $"Time: {Mathf.Max(0, timeLeft):F1}s";
        }
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
    }
}

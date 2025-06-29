using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardUI : MonoBehaviour
{
    [Header("Prefab dòng b?ng x?p h?ng")]
    public GameObject entryTemplate;

    [Header("Panel t?ng level")]
    public Transform Panel_Level1;
    public Transform Panel_Level2;
    public Transform Panel_Level3;

    [Header("S? l??ng t?i ?a dòng hi?n th?")]
    public int maxDisplay = 5;

    private void Start()
    {
        // M?c ??nh ch? hi?n th? b?ng Level1
        Panel_Level1.gameObject.SetActive(false);
        Panel_Level2.gameObject.SetActive(false);
        Panel_Level3.gameObject.SetActive(false);
    }

    public void ShowLevel(string levelName, Transform targetPanel)
    {
        // ?n toàn b? panel tr??c
        Panel_Level1.gameObject.SetActive(false);
        Panel_Level2.gameObject.SetActive(false);
        Panel_Level3.gameObject.SetActive(false);

        // Xóa dòng c?
        foreach (Transform child in targetPanel)
        {
            Destroy(child.gameObject);
        }

        // Load d? li?u
        List<LeaderboardManager.Entry> entries = LeaderboardManager.LoadScores(levelName);

        // T?o dòng m?i
        for (int i = 0; i < Mathf.Min(entries.Count, maxDisplay); i++)
        {
            GameObject entryGO = Instantiate(entryTemplate, targetPanel);
            entryGO.SetActive(true);

            entryGO.transform.Find("TopText").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            entryGO.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = entries[i].name;
            entryGO.transform.Find("TimeText").GetComponent<TextMeshProUGUI>().text = entries[i].time.ToString("F1") + "s";
            entryGO.transform.Find("CherryText").GetComponent<TextMeshProUGUI>().text = entries[i].cherries.ToString();
        }

        targetPanel.gameObject.SetActive(true);
    }

    // Hàm g?i khi b?m nút Level
    public void OnClickLevel1() => ShowLevel("Level1", Panel_Level1);
    public void OnClickLevel2() => ShowLevel("Level2", Panel_Level2);
    public void OnClickLevel3() => ShowLevel("Level3", Panel_Level3);

    public void OnClickBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

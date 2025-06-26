using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermanentUI : MonoBehaviour
{
    public int cherris = 0;
    public int health = 3;

    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI healthCount;

    private Dictionary<string, Vector3> checkpointPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Vector3> defaultSpawnPositions = new Dictionary<string, Vector3>();

    public static PermanentUI perm;

    private void Awake()
    {
        if (!perm)
        {
            perm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUI()
    {
        if (cherryText != null) cherryText.text = cherris.ToString();
        if (healthCount != null) healthCount.text = health.ToString();
    }

    public void AddCherry()
    {
        cherris++;
        UpdateUI();
    }

    public void SubtractHealth()
    {
        health--;
        UpdateUI();
    }

    public void SaveCheckpointState(Vector3 pos)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        checkpointPositions[sceneName] = pos;
    }

    public void SetDefaultSpawn(Vector3 pos)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        defaultSpawnPositions[sceneName] = pos;
    }

    public void LoadCheckpointState(GameObject player)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (checkpointPositions.TryGetValue(sceneName, out Vector3 pos))
        {
            player.transform.position = pos;
        }
        else if (defaultSpawnPositions.TryGetValue(sceneName, out Vector3 fallback))
        {
            player.transform.position = fallback;
        }
    }

    public void FullReset()
    {
        cherris = 0;
        health = 3;
        checkpointPositions.Clear();
        SceneManager.LoadScene("Level1");
        UpdateUI();
    }
}

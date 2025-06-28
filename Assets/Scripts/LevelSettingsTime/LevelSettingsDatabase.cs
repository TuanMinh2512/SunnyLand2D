using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsDatabase", menuName = "Game/Level Settings Database")]
public class LevelSettingsDatabase : ScriptableObject
{
    public LevelSettings[] levels;

    public float GetTimeLimitForScene(string sceneName)
    {
        foreach (var level in levels)
        {
            if (level.sceneName == sceneName)
            {
                return level.timeLimit;
            }
        }

        Debug.LogWarning($"No time setting found for scene '{sceneName}', using default 60s");
        return 60f;
    }
}

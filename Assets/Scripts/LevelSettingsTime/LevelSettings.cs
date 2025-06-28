using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/Level Settings", order = 1)]
public class LevelSettings : ScriptableObject
{
    public string sceneName;
    public float timeLimit = 60f;
}

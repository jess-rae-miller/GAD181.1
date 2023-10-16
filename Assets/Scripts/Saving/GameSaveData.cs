using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor.SceneManagement;

[System.Serializable]
public class GameSaveData {
      // Saved Data
    public int totalDeaths;
    public int sceneDeaths;

    public Dictionary<string, float> sceneTimes;

    public GameSaveData()
    {
        sceneTimes = new Dictionary<string, float>();
    }
    public void ResetSceneDeaths()
    {
        totalDeaths = 0;
    }
}
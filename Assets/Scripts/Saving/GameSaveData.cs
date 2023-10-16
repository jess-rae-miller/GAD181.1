using System.Collections.Generic;

[System.Serializable]
public class GameSaveData
{
    public int totalDeaths;
    public int sceneDeaths;

    public Dictionary<string, float> sceneTimes;

    public GameSaveData()
    {
        if (sceneTimes == null)
        {
            sceneTimes = new Dictionary<string, float>();
        }
    }

    public void ResetSceneDeaths()
    {
        totalDeaths = 0;
    }
}

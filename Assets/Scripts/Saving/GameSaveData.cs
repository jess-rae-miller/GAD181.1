[System.Serializable]
public class GameSaveData
{
    public int totalDeaths;
    public int sceneDeaths;

    public void ResetSceneDeaths()
    {
        sceneDeaths = 0; // Reset scene deaths to 0
    }
}


using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath = "save.dat";

    public void SaveGame(GameSaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(GetSavePath());
        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public GameSaveData LoadGame()
    {
        if (File.Exists(GetSavePath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(GetSavePath(), FileMode.Open);
            GameSaveData data = (GameSaveData)formatter.Deserialize(fileStream);
            fileStream.Close();
            return data;
        }
        else
        {
            return new GameSaveData();
        }
    }

    public void ResetSceneDeaths()
    {
        GameSaveData savedData = LoadGame();
        savedData.ResetSceneDeaths();
        SaveGame(savedData);
    }

    public void ResetGame()
    {
        if (File.Exists(GetSavePath()))
        {
            File.Delete(GetSavePath());
        }

        // Reset the stats or other game data to their initial values
        GameSaveData initialData = new GameSaveData(); // Set initial values here
        SaveGame(initialData);
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, saveFilePath);
    }
}


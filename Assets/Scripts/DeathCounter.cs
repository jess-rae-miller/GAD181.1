using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public TextMeshPro totalDeathText;  // Reference to a Text component for total deaths
    public TextMeshPro sceneDeathText;  // Reference to a Text component for deaths in this scene

    private int totalDeaths;
    private int sceneDeaths;
    private SaveManager saveManager;

    private void Start()
    {
        saveManager = GetComponent<SaveManager>();

        LoadSavedData();
        UpdateDeathText();
    }

    private void IncreaseTotalDeathCount()
    {
        totalDeaths++;
        SaveData();
        UpdateDeathText();
    }

    public void IncreaseSceneDeathCount()
    {
        sceneDeaths++;
        IncreaseTotalDeathCount();
        UpdateDeathText();
    }

    private void UpdateDeathText()
    {
        // Update the Text components to display the current death counts
        totalDeathText.text = "" + totalDeaths.ToString();
        sceneDeathText.text = "" + sceneDeaths.ToString();
    }

    private void LoadSavedData()
    {
        GameSaveData savedData = saveManager.LoadGame();
        totalDeaths = savedData.totalDeaths;
        sceneDeaths = savedData.sceneDeaths;
    }

    public void ResetSceneDeaths()
    {
        sceneDeaths = 0;
        SaveData();
    }

    private void SaveData()
    {
        GameSaveData savedData = new GameSaveData();
        savedData.totalDeaths = totalDeaths;
        savedData.sceneDeaths = sceneDeaths;
        saveManager.SaveGame(savedData);
    }
}

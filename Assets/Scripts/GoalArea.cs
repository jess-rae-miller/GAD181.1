using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class GoalArea : MonoBehaviour
{
    private CountdownTimer timer;
    private GameSaveData gameSaveData;
    private int currentSceneNumber; // Store the scene number as an integer
    private SaveManager saveManager;
    void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();

        timer = FindAnyObjectByType<CountdownTimer>();

        // Load the saved game data
        gameSaveData = LoadSavedData();

        // Use regular expression to extract the number from the scene name
        currentSceneNumber = ExtractSceneNumber(SceneManager.GetActiveScene().name);
    }

    private int ExtractSceneNumber(string sceneName)
    {
        // Use regular expression to match and extract the number
        Match match = Regex.Match(sceneName, @"\d+");
        if (match.Success)
        {
            return int.Parse(match.Value) - 1;
        }
        else
        {
            Debug.LogWarning("Scene name doesn't contain a number.");
            return -1; // Indicate failure
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer.isActive = false;

            // Get the current scene's name
            string currentSceneName = SceneManager.GetActiveScene().name;

            // Check if there's a saved best time for this scene
            if (gameSaveData.sceneTimes.ContainsKey(currentSceneName))
            {
                float savedBestTime = gameSaveData.sceneTimes[currentSceneName];

                // Compare the current time with the saved best time
                if (timer.currentTime > savedBestTime)
                {
                    // Update the saved best time
                    gameSaveData.sceneTimes[currentSceneName] = timer.currentTime;
                    // Save the updated data
                    SaveGame(gameSaveData);
                }
            }
            else
            {
                // If no saved best time exists, save the current time
                gameSaveData.sceneTimes[currentSceneName] = timer.currentTime;
                // Save the data
                SaveGame(gameSaveData);
            }

            // Load the next scene
            int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            {
                gameSaveData.ResetSceneDeaths();
                SceneManager.LoadScene(nextSceneBuildIndex);
            }
            else
            {
                Debug.LogWarning("No next scene available.");
            }
        }
    }

    // Function to load saved data (you can implement this according to your SaveManager)
    private GameSaveData LoadSavedData()
    {
        // Implement loading your saved data (GameSaveData) here using your SaveManager
        return saveManager.LoadGame(); // Replace with the correct method from your SaveManager
    }

    // Function to save the game data (you can implement this according to your SaveManager)
    private void SaveGame(GameSaveData data)
    {
        // Implement saving your game data (GameSaveData) here using your SaveManager
        saveManager.SaveGame(data); // Replace with the correct method from your SaveManager
    }
}

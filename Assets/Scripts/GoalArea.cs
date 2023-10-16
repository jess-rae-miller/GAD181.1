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

    void Start()
    {
        timer = FindAnyObjectByType<CountdownTimer>();
        gameSaveData = new GameSaveData(); // Initialize the GameSaveData instance

        // Use regular expression to extract the number from the scene name
        currentSceneNumber = ExtractSceneNumber(SceneManager.GetActiveScene().name);
    }

    private int ExtractSceneNumber(string sceneName)
    {
        // Use regular expression to match and extract the number
        Match match = Regex.Match(sceneName, @"\d+");
        if (match.Success)
        {
            return int.Parse(match.Value)-1;
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

            // Check if the current time and death count set a new high score
            var currentPlayerStats = (gameSaveData.sceneDeaths, timer.currentTime);
            var currentHighScore = gameSaveData.GetHighscoreForLevel(currentSceneNumber); // Use the extracted scene number

            if (currentHighScore.Item1 == -1 || // No previous high score
                currentPlayerStats.Item2 < currentHighScore.Item2 || // New best time
                (currentPlayerStats.Item2 == currentHighScore.Item2 && currentPlayerStats.Item1 < currentHighScore.Item1)) // Same time but fewer deaths
            {
                // Update the high score
                gameSaveData.SetHighscoreForLevel(currentSceneNumber, currentPlayerStats); // Use the extracted scene number
            }

            // Get the build index of the next scene
            int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Check if there's a scene at the next build index
            if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Load the next scene
                gameSaveData.ResetSceneDeaths();
                SceneManager.LoadScene(nextSceneBuildIndex);
            }
            else
            {
                Debug.LogWarning("No next scene available.");
            }
        }
    }
}

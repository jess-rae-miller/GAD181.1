using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshPro highScoreText;  // Reference to a TextMeshPro component for displaying the highest score
    private SaveManager saveManager; // Reference to your SaveManager script

    private void Awake()
    {
        // Find the GameObject with the SaveManager script and get a reference to it
        saveManager = FindObjectOfType<SaveManager>();
    }

    private void Start()
    {
        if (saveManager != null)
        {
            // Load the saved data
            GameSaveData savedData = saveManager.LoadGame();

            // Get the current scene name
            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log(savedData != null);
            Debug.Log(sceneName);
            Debug.Log(savedData.sceneTimes != null) ;
            // Check if the scene has a high score
            if (savedData.sceneTimes.ContainsKey(sceneName))
            {
                // Get the highest score for the current scene
                float highestScore = savedData.sceneTimes[sceneName];
                highScoreText.text = highestScore.ToString("F2"); // Format the time to display
            }
            else
            {
                // If there is no high score for the scene, display a default message
                highScoreText.text = "N/A";
            }
        }
        else
        {
            Debug.LogError("SaveManager not found in the scene.");
        }
    }
}

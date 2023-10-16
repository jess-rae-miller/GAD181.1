using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    private TextMeshPro highscoreText;
    private GameSaveData gameSaveData;

    void Start()
    {
        highscoreText = GetComponent<TextMeshPro>();
        gameSaveData = new GameSaveData(); // Initialize the GameSaveData instance
        UpdateHighscoreText();
    }

    public void UpdateHighscoreText() {

        // Retrieve the high score for the current level
        var highscore = gameSaveData.GetHighscoreForLevel(gameSaveData.GetSceneNumber(SceneManager.GetActiveScene().name));

        if (highscore.Item1 != -1 && highscore.Item2 != -1f)
        {
            highscoreText.text = "High Score\nDeaths: " + highscore.Item1 + "\nTime: " + highscore.Item2.ToString("F2");
        }
        else
        {
            highscoreText.text = "High Score\nN/A";
        }
    }
}

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor.SceneManagement;


[System.Serializable]
public class GameSaveData
{
    // Internal use only
    private int numberOfLevels = 10;

    // Saved Data
    public int totalDeaths;
    public int sceneDeaths;
    public List<(int, float)> levelHighscores = new List<(int, float)>(); // (death, time)

    public GameSaveData()
    {
        // Initialize the high score list with default values (e.g., (-1, -1) for no score)
        for (int i = 0; i < numberOfLevels; i++)
        {
            levelHighscores.Add((-1, -1f));
        }
    }

    public void ResetSceneDeaths()
    {
        sceneDeaths = 0; // Reset scene deaths to 0
    }

    public (int deathCount, float finishTime) GetHighscoreForLevel(int level)
    {
        if (level >= 0 && level < levelHighscores.Count)
        {
            return levelHighscores[level];
        }
        else 
        {
            return (-1, -1f); // Invalid level index
        }
    }
    public int GetSceneNumber(string sceneName)
    {
        // Use regular expression to match and extract the number
        Match match = Regex.Match(sceneName, @"\d+");
        if (match.Success)
        {
            return int.Parse(match.Value);
        }
        else
        {
            Debug.LogWarning("Scene name doesn't contain a number.");
            return -1; // Indicate failure
        }
    }


    public void SetHighscoreForLevel(int level, (int deathCount, float finishTime) score)
    {
        if (level >= 0 && level < levelHighscores.Count)
        {
            var currentHighscore = levelHighscores[level];

            // Check if the new score is better (lower death count or shorter time)
            if (score.Item1 < currentHighscore.Item1 || currentHighscore.Item1 == -1)
            {
                levelHighscores[level] = score;
            }
            else if (score.Item1 == currentHighscore.Item1 && (score.Item2 < currentHighscore.Item2 || currentHighscore.Item2 == -1f))
            {
                levelHighscores[level] = score;
            }
        }
    }
}

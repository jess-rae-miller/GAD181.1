using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalArea : MonoBehaviour
{
    private CountdownTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindAnyObjectByType<CountdownTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer.isActive = false;
            // Get the current active scene
            Scene currentScene = SceneManager.GetActiveScene();

            // Get the build index of the next scene
            int nextSceneBuildIndex = currentScene.buildIndex + 1;

            // Check if there's a scene at the next build index
            if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Load the next scene
                FindAnyObjectByType<DeathCounter>().ResetSceneDeaths();
                SceneManager.LoadScene(nextSceneBuildIndex);
            }
            else
            {
                Debug.LogWarning("No next scene available.");
            }
        }
    }
}
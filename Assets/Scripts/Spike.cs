using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    [SerializeField] private float extensionSpeed = 2.0f; // Time it takes to scale from 0 to the saved Y-axis value
    [SerializeField] private float retractionTime = 1.0f; // Time it takes to scale from 0 to the saved Y-axis value
    [SerializeField] private float retractedPauseTime = 0.1f;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(ContinuousInOut());
    }

    // Function to smoothly extend the spike
    private IEnumerator Extend()
    {
        float elapsedTime = 0f;

        while (elapsedTime < extensionSpeed)
        {
            float scaleFactor = Mathf.Lerp(0f, initialScale.y, elapsedTime / extensionSpeed);
            transform.localScale = new Vector3(initialScale.x, scaleFactor, initialScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the scale reaches exactly the initial Y-axis value at the end
        transform.localScale = initialScale;
    }

    // Function to smoothly retract the spike
    private IEnumerator Retract()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = new Vector3(initialScale.x, 0f, initialScale.z);

        while (elapsedTime < retractionTime)
        {
            float scaleFactor = Mathf.Lerp(initialScale.y, 0f, elapsedTime / retractionTime);
            transform.localScale = new Vector3(initialScale.x, scaleFactor, initialScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the scale reaches exactly 0 at the end
        transform.localScale = targetScale;
    }

    private void RestartCurrentLevel()
    {
        // Get the current active scene's name.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Reload (restart) the current scene.
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RestartCurrentLevel();
        }
    }

    // Coroutine to continuously extend and retract the spike
    private IEnumerator ContinuousInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Extend());
            //yield return new WaitForSeconds(scalingTime);
            yield return StartCoroutine(Retract());
            yield return new WaitForSeconds(retractedPauseTime);
        }
    }
}

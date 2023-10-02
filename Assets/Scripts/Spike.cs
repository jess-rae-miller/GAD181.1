using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    public float scalingTime = 2.0f; // Time it takes to scale from 0 to the saved Y-axis value
    [SerializeField] private float delayTime = 1f;
    private float scaleStartTime;
    private bool isScaling = false;
    private Vector3 initialScale;

    private void Start()
    {
        scaleStartTime = Time.time;
        initialScale = transform.localScale;
        StartCoroutine(ContinuousInOut());
    }

    private void Update()
    {
        if (isScaling)
        {
            if (Time.time - scaleStartTime < scalingTime)
            {
                // Calculate the current scale factor based on time
                float scaleFactor = (Time.time - scaleStartTime) / scalingTime;

                // Set the local scale of the object to the calculated scale factor
                transform.localScale = new Vector3(initialScale.x, scaleFactor * initialScale.y, initialScale.z);
            }
            else
            {
                // Ensure that the scale reaches exactly the initial Y-axis value at the end
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
                isScaling = false;
            }
        }
    }

    // Function to extend the spike
    public void Extend()
    {
        if (!isScaling)
        {
            scaleStartTime = Time.time;
            isScaling = true;
        }
    }

    // Function to retract the spike
    public void Retract()
    {
        if (isScaling)
        {
            // Reverse the scaling process
            float currentScaleFactor = (Time.time - scaleStartTime) / scalingTime;
            float reversedScaleFactor = 1.0f - currentScaleFactor;
            transform.localScale = new Vector3(initialScale.x, reversedScaleFactor * initialScale.y, initialScale.z);
            isScaling = false;
        }
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
            Debug.Log("hehe");
        }
    }

    // Coroutine to continuously extend and retract the spike
    private IEnumerator ContinuousInOut()
    {
        while (true)
        {
            Extend();
            yield return new WaitForSeconds(scalingTime);
            Retract();
            yield return new WaitForSeconds(delayTime); // Wait for the specified delayTime
        }
    }
}

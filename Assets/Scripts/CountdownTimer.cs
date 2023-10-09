using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 10f;
    private float currentTime;
    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private Vector2 direction = Vector2.up;
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    private Vector3 originalSize;
    public bool isActive = false;
    private Renderer objectRenderer; // Reference to the object's renderer component

    private float visualStartTime; // The time at which the visual timer starts

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        visualStartTime = startTime;
        originalSize = transform.localScale;
        objectRenderer = GetComponent<Renderer>(); // Get the renderer component
        UpdateText();
        objectRenderer.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isActive);
        if (isActive)
        {
            currentTime -= Time.deltaTime;
            UpdateText();
            if (currentTime <= 0f)
            {
                // Timer has expired, stop shrinking
                isActive = false;
                currentTime = 0f;
                RestartCurrentLevel();
            }

            // Calculate the new scale based on the timer
            float scaleAmount = Mathf.Clamp(1 - (currentTime / visualStartTime), 0f, 1f); // Shrink from 1 to 0

            // Calculate the new scale on the specified axis
            Vector3 newScale = new Vector3(
                originalSize.x - direction.x * scaleAmount * originalSize.x,
                originalSize.y - direction.y * scaleAmount * originalSize.y,
                originalSize.z
            );

            // Apply the new scale
            transform.localScale = newScale;

            // Interpolate the color based on the timer
            Color lerpedColor = Color.Lerp(startColor, endColor, scaleAmount);

            // Apply the interpolated color to the object's renderer
            objectRenderer.material.color = lerpedColor;
        }
        else
        {

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseTimer(20f); // Increase the timer by 60 seconds (1 minute)
        }
    }

    private void RestartCurrentLevel()
    {
        // Get the current active scene's name.
        string currentSceneName = SceneManager.GetActiveScene().name;

        //Increase scene deaths
        FindAnyObjectByType<DeathCounter>().IncreaseSceneDeathCount();

        // Reload (restart) the current scene.
        SceneManager.LoadScene(currentSceneName);
    }

    public void IncreaseTimer(float increase)
    {
        currentTime += increase;

        // Update visualStartTime to match the new currentTime if currentTime exceeds it
        if (currentTime > visualStartTime)
        {
            visualStartTime = currentTime;
        }
    }

    private void UpdateText()
    {
        timerText.text = Math.Round(currentTime, 1).ToString();
    }
}

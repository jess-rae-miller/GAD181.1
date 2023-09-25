using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;
    private float startingTime = 10f;
    public bool isActive = true;

    [SerializeField] private TextMeshProUGUI countdownText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (isActive)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = Math.Round(currentTime, 1).ToString() + " Sec";

            if (currentTime <= 0)
            {
                RestartLevel();
            }
        }
    }

    private void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

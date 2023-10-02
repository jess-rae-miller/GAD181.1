using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float scalingTime = 2.0f; // Time it takes to scale from 0 to the saved Y-axis value
    private float scaleStartTime;
    private bool isScaling = false;
    private Vector3 initialScale;

    private void Start()
    {
        scaleStartTime = Time.time;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (!isScaling && Time.time - scaleStartTime < scalingTime)
        {
            // Calculate the current scale factor based on time
            float scaleFactor = (Time.time - scaleStartTime) / scalingTime;
            
            // Set the local scale of the object to the calculated scale factor
            transform.localScale = new Vector3(initialScale.x, scaleFactor * initialScale.y, initialScale.z);
        }
        else if (!isScaling)
        {
            // Ensure that the scale reaches exactly the initial Y-axis value at the end
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
            isScaling = true;
        }
    }
}

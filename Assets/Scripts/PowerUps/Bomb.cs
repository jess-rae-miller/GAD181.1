using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class Bomb : MonoBehaviour
{
    [SerializeField] private float totalExplosionTime = 5.0f; // Total time before explosion
    [SerializeField] private float explosionRadius = 2.0f;
    [SerializeField] private SpriteRenderer bombRenderer;
    [SerializeField] private TextMeshPro timeText; // Reference to TextMeshPro component
    [SerializeField] private GameObject explosionPrefab;
    private Color originalColor;
    private bool isExploding = false;
    private float blinkInterval;
    private float explosionScheduledTime; // Store the time when the explosion is scheduled

    void Start()
    {
        bombRenderer = GetComponent<SpriteRenderer>();
        originalColor = bombRenderer.color;

        // Calculate the initial blink interval based on total time and number of blinks
        int numBlinks = Mathf.FloorToInt(totalExplosionTime / 0.1f); // 0.1f is the blink duration
        blinkInterval = totalExplosionTime / (2 * numBlinks);

        // Set the scheduled explosion time
        explosionScheduledTime = Time.time + totalExplosionTime;

        // Invoke the countdown and explosion
        InvokeRepeating("ToggleBlink", 0.0f, blinkInterval);
        Invoke("Explode", totalExplosionTime);
    }

    void Update()
    {
        // Update the TextMeshPro text to display the rounded time until explosion
        if (!isExploding)
        {
            float timeLeft = Mathf.Max(0, explosionScheduledTime - Time.time);
            timeText.text = timeLeft.ToString("F0"); // "F0" rounds to a whole number
        }
    }


    void ToggleBlink()
    {
        if (!isExploding)
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        Color redColor = new Color(1.0f, 0.0f, 0.0f); // Vibrant red
        bombRenderer.color = redColor;
        yield return new WaitForSeconds(0.1f); // Blink duration

        bombRenderer.color = originalColor;
    }

    IEnumerator CountdownToExplosion(float explosionScheduledTime)
    {
        while (Time.time < explosionScheduledTime)
        {
            yield return null;
        }
        Explode();
    }

    void Explode()
    {
        isExploding = true;
        CancelInvoke("ToggleBlink");
        StartCoroutine(ExponentialBlink());
    }

    IEnumerator ExponentialBlink()
    {
        float timeLeft = totalExplosionTime - Time.time;
        while (timeLeft > 0.1f)
        {
            StartCoroutine(Blink());
            yield return new WaitForSeconds(blinkInterval);
            timeLeft = totalExplosionTime - Time.time;
        }

        // Final explosion
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D col in colliders)
        {
            Debug.Log(col.name);
            if (col.CompareTag("Walls")) // Check if the object has the "Walls" tag
            {
                col.gameObject.SetActive(false);
            }
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

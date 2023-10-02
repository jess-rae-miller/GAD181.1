using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasePowerUp : PowerUp
{
    [SerializeField] private float phaseDuration = 1f;
    private bool isPowerActive = false;

    protected override void ActivatePower()
    {
        if (!isPowerActive)
        {
            isPowerActive = true;

            // Reduce opacity to 50%
            Color newColor = playerRenderer.color;
            newColor.a = 0.5f;
            playerRenderer.color = newColor;

            // Disable the collider
            playerCollider.enabled = false;

            // Start a timer to re-enable the collider and restore opacity
            StartCoroutine(DeactivatePowerAfterDuration());
        }
    }

    private IEnumerator DeactivatePowerAfterDuration()
    {
        yield return new WaitForSeconds(phaseDuration);

        // Restore original opacity and re-enable the collider
        Color newColor = playerRenderer.color;
        newColor.a = 1f;
        playerRenderer.color = newColor;

        playerCollider.enabled = true;

        isPowerActive = false;
    }
}

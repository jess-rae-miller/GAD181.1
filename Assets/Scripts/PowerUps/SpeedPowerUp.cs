using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] private float speedDuration = 1f;
    private bool isPowerActive = false;
    private float startMovementSpeed;

    protected override void ActivatePower()
    {
        if (!isPowerActive)
        {
            isPowerActive = true;
            startMovementSpeed = playerMovement.moveSpeed;
            playerMovement.moveSpeed = playerMovement.moveSpeed * 2;

            // Start a timer to re-enable the collider and restore opacity
            StartCoroutine(DeactivatePowerAfterDuration());
        }
    }

    private IEnumerator DeactivatePowerAfterDuration()
    {
        yield return new WaitForSeconds(speedDuration);

        isPowerActive = false;
        playerMovement.moveSpeed = startMovementSpeed;
    }
}

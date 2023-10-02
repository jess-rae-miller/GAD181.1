using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] private float speedDuration = 1f;
    [SerializeField] private float speedMultiplier = 2f;
    private bool isPowerActive = false;
    private float startMovementSpeed;
    private ShadowTrail playerShadow;

    protected override void ActivatePower()
    {
        playerShadow = player.GetComponent<ShadowTrail>();
        if (!isPowerActive)
        {
            isPowerActive = true;
            startMovementSpeed = playerMovement.moveSpeed;
            playerMovement.moveSpeed = playerMovement.moveSpeed * speedMultiplier;
            playerShadow.isShadowTrailActive = true;
            // Start a timer to re-enable the collider and restore opacity
            StartCoroutine(DeactivatePowerAfterDuration());
        }
    }

    private IEnumerator DeactivatePowerAfterDuration()
    {
        yield return new WaitForSeconds(speedDuration);
        playerShadow.isShadowTrailActive = false;
        isPowerActive = false;
        playerMovement.moveSpeed = startMovementSpeed;
    }
}

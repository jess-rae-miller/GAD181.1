using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : PowerUp
{
    protected override void ActivatePower()
    {
        playerMovement.bombCount++;
        Destroy(gameObject);
    }
}

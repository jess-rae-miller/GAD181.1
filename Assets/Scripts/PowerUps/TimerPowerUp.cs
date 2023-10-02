using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPowerUp : PowerUp
{
    [SerializeField] private float timerIncreaseAmount = 1f;
    private CountdownTimer timer;

    protected override void ActivatePower()
    {
        timer = FindAnyObjectByType<CountdownTimer>();
        timer.IncreaseTimer(timerIncreaseAmount);
    }
}

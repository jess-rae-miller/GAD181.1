using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPowerUp : PowerUp
{
    [SerializeField] private float timerIncreaseAmount = 1f;
    private CountdownTimer timer;

    private void Start()
    {
        timer = FindAnyObjectByType<CountdownTimer>();
    }
    protected override void ActivatePower()
    {
        timer.IncreaseTimer(timerIncreaseAmount);
    }
}

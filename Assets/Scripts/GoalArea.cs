using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private CountdownTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindAnyObjectByType<CountdownTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
         timer.isActive = false;
        }
    }
}

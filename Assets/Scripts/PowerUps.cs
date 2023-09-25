using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float timerIncreaseAmount = 1f;
    private CountdownTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindAnyObjectByType<CountdownTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer.currentTime += timerIncreaseAmount;
            Destroy(gameObject);
        }
    }
}

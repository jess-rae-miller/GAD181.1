using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
    {
        Vector3 movementDirection = Vector3.down * speed * Time.deltaTime;
        transform.Translate(movementDirection, Space.World);
    }
}

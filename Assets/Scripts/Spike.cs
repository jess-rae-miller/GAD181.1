using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Vector3 speed;

    private void Update()
    {
        Vector3 movementDirection = Vector3.down * speed * Time.deltaTime;
        transform.Translate(movementDirection, Space.World);
    }
}

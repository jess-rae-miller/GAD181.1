using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public int level;
    private int bombcount;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        // Check if the player has moved
        if (moveDirection != Vector2.zero)
        {
            FindAnyObjectByType<CountdownTimer>().isActive = true;
        }

        /*// Check if the player's position is outside the boundary
        if (!isInBoundary())
        {
            // Player is out of bounds, restart the level
            SceneManager.LoadScene("Level_" + level.ToString());
        }*/
    }

        /*static void Main(string[] args)
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.E)
            {
                // Call your function here
                BombExplosion();
            }
        }
    }
void BombExplosion()
    {
        if (bombcount >= 1)
        {

        }
    }*/
    void FixedUpdate()
    {
        Move();
    }

    // Movement on X & Y axis
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    /*private bool isInBoundary()
    {
        // Get the boundary collider's position and size
        Collider2D boundaryCollider = GameObject.Find("Boundary").GetComponent<Collider2D>();

        // Get the player's position
        Vector2 playerPosition = transform.position;

        // Check if the player's position is within the boundary
        if (playerPosition.x >= boundaryCollider.bounds.min.x &&
            playerPosition.x <= boundaryCollider.bounds.max.x &&
            playerPosition.y >= boundaryCollider.bounds.min.y &&
            playerPosition.y <= boundaryCollider.bounds.max.y)
        {
            return true;
        }

        return false;
    }*/
}

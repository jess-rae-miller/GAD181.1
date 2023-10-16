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
    public int bombCount = 0;
    [SerializeField] private GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlaceBomb();
        }
        // Check if the player has moved
        if (moveDirection != Vector2.zero)
        {
            FindAnyObjectByType<CountdownTimer>().isActive = true;
        }

        // Check if the player's position is outside the boundary
        if (!isInBoundary())
        {
            //Scene currentScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(currentScene.name);
        }
    }

    private void PlaceBomb()
    {
        if(bombCount > 0)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bombCount--;
        }
    }

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

    private bool isInBoundary()
    {
        // Cast a ray from the player's current position to a direction (e.g., Vector2.down).
        // Adjust the direction based on your game's design.
        Vector2 rayDirection = Vector2.down; // You can change this direction as needed.

        // Perform a raycast on all layers.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1f);

        // Check if the ray hit any object with the "Boundary" tag.
        if (hit.collider != null && hit.collider.CompareTag("Boundary"))
        {
            return true; // Player is inside the boundary.
        }

        return false; // Player is outside the boundary.
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //Jess dont touch this script unless you know what youre doing plz xoxo
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public GameObject player;
    [HideInInspector] public SpriteRenderer playerRenderer;
    [HideInInspector] public Collider2D playerCollider;

    private void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        player = playerMovement.gameObject;
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerCollider = player.GetComponent<Collider2D>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivatePower();
            Destroy(gameObject);
        }
    }
    protected virtual void ActivatePower()
    {
        Debug.Log("Warning: Incorrect Implementation");
    }
}

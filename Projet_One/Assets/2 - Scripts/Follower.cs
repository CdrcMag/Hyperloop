using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform player;

    private Rigidbody2D rb;

    public Color32 currentColor;

    private SpriteRenderer playerRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rb.MovePosition(player.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fix")
        {
            currentColor = collision.GetComponent<SpriteRenderer>().color;
            //SetPlayerColor(currentColor);
        }
    }

    private void SetPlayerColor(Color32 c)
    {
        playerRenderer.color = currentColor;
    }
}

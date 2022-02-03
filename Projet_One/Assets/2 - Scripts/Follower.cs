using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform player;

    private Rigidbody2D rb;

    public Color32 currentColor;

    private SpriteRenderer playerRenderer;
    private TrailRenderer trail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerRenderer = player.GetComponent<SpriteRenderer>();
        trail = player.GetChild(0).GetComponent<TrailRenderer>();
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
            SetPlayerColor(currentColor);

            if(collision.GetComponent<Animator>() != null)
                collision.GetComponent<Animator>().SetTrigger("Turn");
        }
    }

    private void SetPlayerColor(Color32 c)
    {
        playerRenderer.color = currentColor;

        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[]
              {
                      new GradientColorKey(c, 0.0f),
                      new GradientColorKey(c, 1.0f) },
              new GradientAlphaKey[] {
                      new GradientAlphaKey(1.0f, 0.0f)
              });

        trail.colorGradient = grad;
    }
}

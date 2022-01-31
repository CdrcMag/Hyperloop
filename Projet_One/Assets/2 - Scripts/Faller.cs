using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Rigidbody2D))]
public class Faller : MonoBehaviour
{
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private Light2D m_light;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
        m_light = GetComponentInChildren<Light2D>();

    }

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        var mainColor = ps.colorOverLifetime;
        mainColor.enabled = true;

        //Color handling
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[]
              {
                new GradientColorKey(GetComponent<SpriteRenderer>().color, 0.0f),
                new GradientColorKey(GetComponent<SpriteRenderer>().color, 1.0f) },
                new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 0.8f),
                new GradientAlphaKey(1.0f, 1.0f)
              });

        mainColor.color = grad;

        m_light.color = GetComponent<SpriteRenderer>().color;
    }

    [HideInInspector] public float speed = 3;

    private void Update()
    {
        rb.MovePosition(rb.position + Vector2.down * speed * Time.deltaTime);
    }

}

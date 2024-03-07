using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSprites : MonoBehaviour
{
    public static PlayerSprites Instance { get; private set; }

    [HideInInspector] public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float vel = rb.velocity.x;

        if (vel > 0)
            spriteRenderer.flipX = false;
        else if (vel < 0)
        {
            vel *= -1;
            spriteRenderer.flipX = true;
        }

        anim.SetFloat("x_velocity", vel);
        anim.SetFloat("y_velocity", rb.velocity.y);
    }
}
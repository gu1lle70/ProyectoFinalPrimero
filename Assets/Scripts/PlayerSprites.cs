using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSprites : MonoBehaviour
{
    public static PlayerSprites Instance { get; private set; }

    public SpriteRenderer spriteRenderer;
    public int facingDirection;

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

        facingDirection = 1;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float vel_y = rb.velocity.y;

        if (WallJump.instance.sliding)
        {
            anim.SetBool("sliding", true);
            return;
        }
        else if (anim.GetBool("sliding"))
        {
            anim.SetBool("sliding", false);
        }

        anim.SetBool("grounded", PhysicsManager.Instance.IsGrounded);

        UpdateFacingDirection(PlayerMove.Instance._dir.x);

        float vel = Mathf.Abs(rb.velocity.x);
        if (PlayerMove.Instance._dir.x == 0 && !DASH.instance.isDashing)
            vel = 0;

        anim.SetFloat("x_velocity", vel);
        anim.SetFloat("y_velocity", vel_y);
    }

    private void UpdateFacingDirection(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
        }
    }
}
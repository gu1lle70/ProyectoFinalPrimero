using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JUMP : MonoBehaviour
{
    public static JUMP Instance { get; private set; }
    [Header("Jump sound")]
    [SerializeField] private AudioClip jump_sound;

    [Header("Jump stats")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float maxJumpTime = 0.5f;

    [Header("Gravity stats")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Jump dust")]
    public ParticleSystem dust;

    [Header("Coyote time")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Wall Jump stats")]
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 1);
    [SerializeField] private LayerMask wallLayer;
    private bool isWallSliding;
    private bool isJumping;

    private Rigidbody2D rb;
    private PlayerInput input;
    private bool jumpHeld;
    private float jumpTimeCounter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        input = new PlayerInput();
        input.Player.Enable();

        input.Player.Jump.performed += Jump_performed;
        input.Player.Jump.canceled += ctx => jumpHeld = false;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!PhysicsManager.Instance.IsGrounded)
        {
            coyoteTimeCounter += Time.deltaTime;
        }
        else
        {
            coyoteTimeCounter = 0;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumpHeld)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Check for wall sliding
        isWallSliding = IsWallSliding();

        if (isWallSliding && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallJumpForce);
        }

        // Maintain jump height based on jump hold time
        if (isJumping)
        {
            if (jumpHeld && jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    public void CreateDust()
    {
        dust.Play();
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (PlayerMove.Instance.isNotInTutorial)
        {
            if (PhysicsManager.Instance.IsGrounded || coyoteTimeCounter < coyoteTime || isWallSliding)
            {
                isJumping = true;
                jumpHeld = true;
                jumpTimeCounter = maxJumpTime; // reiniciar el contador de tiempo de salto
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                GameManager.Instance.GenerateSound(jump_sound);
                CreateDust();
                coyoteTimeCounter = 0;

                // Wall jump
                if (isWallSliding)
                {
                    rb.velocity = new Vector2(wallJumpDirection.x * wallJumpForce * -Mathf.Sign(transform.localScale.x), wallJumpForce);
                    isWallSliding = false;
                }
            }
        }
    }

    private bool IsWallSliding()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, wallLayer) && !PhysicsManager.Instance.IsGrounded;
    }
}
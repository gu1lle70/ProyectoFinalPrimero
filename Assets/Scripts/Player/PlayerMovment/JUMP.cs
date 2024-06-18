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
    private bool isJumping;
    private Rigidbody2D rb;
    private PlayerInput input;
    private bool jumpHeld;
    private float jumpTimeCounter;

    [Header("Gravity stats")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Jump dust")]
    public ParticleSystem dust;

    [Header("Coyote time")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private bool wasGrounded;

    private DASH dashScript;

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
        dashScript = DASH.instance;
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

        if (isJumping && dashScript.isDashing)
        {
            rb.velocity = dashScript.GetDashDirection() * dashScript.dashingPower * dashScript.superDashMultiplier;
            isJumping = false;
            dashScript.isDashing = false;
        }

        if (!wasGrounded && PhysicsManager.Instance.IsGrounded)
        {
            CreateDust();
        }
        wasGrounded = PhysicsManager.Instance.IsGrounded;
    }

    public void CreateDust()
    {
        dust.Play();
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (PlayerMove.Instance.isNotInTutorial)
        {
            if (PhysicsManager.Instance.IsGrounded || coyoteTimeCounter < coyoteTime)
            {
                isJumping = true;
                jumpHeld = true;
                jumpTimeCounter = maxJumpTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                GameManager.Instance.GenerateSound(jump_sound);
                CreateDust();
                coyoteTimeCounter = 0;
            }
        }
    }
}
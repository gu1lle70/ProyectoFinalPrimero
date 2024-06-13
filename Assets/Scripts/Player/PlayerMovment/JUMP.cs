using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JUMP : MonoBehaviour
{
    [Header("Jump sound")]
    [SerializeField] private AudioClip jump_sound;

    [Header("Jump stats")]
    [SerializeField] private float _jumpForce = 16f;

    [Header("Gravity stats")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Jump dust")]
    public ParticleSystem dust;

    [Header("Coyote time")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private Rigidbody2D rb;
    private PlayerInput _input;

    private void Start()
    {
        _input = new PlayerInput();
        _input.Player.Enable();

        _input.Player.Jump.performed += Jump_performed;

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
        else if (rb.velocity.y > 0 )
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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
            if (PhysicsManager.Instance.IsGrounded || coyoteTimeCounter < coyoteTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
                GameManager.Instance.GenerateSound(jump_sound);
                CreateDust();
                coyoteTimeCounter = 0;
            }
        }
    }

   
}
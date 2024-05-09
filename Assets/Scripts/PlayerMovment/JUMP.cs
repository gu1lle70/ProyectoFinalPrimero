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

    [Header("Coyote time")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;
    Rigidbody2D rb;

    PlayerInput _input;
    private void Start()
    {
        _input = new PlayerInput();
        _input.Player.Enable();
        
        _input.Player.Jump.performed += Jump_performed;
        _input.Player.Jump.canceled += Jump_canceled;

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
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (PlayerMove.Instance.isNotInTutorial)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0;
        }
    }

    private void Jump_canceled(InputAction.CallbackContext context)
    {
        if (PlayerMove.Instance.isNotInTutorial)
        {
            if (PhysicsManager.Instance.IsGrounded || coyoteTimeCounter < coyoteTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
                GameManager.GenerateSound(jump_sound);
                coyoteTimeCounter = 0;
            }
        }
    }
}
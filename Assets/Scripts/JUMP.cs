using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JUMP : MonoBehaviour
{
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private AudioClip jump_sound;
    [SerializeField] private float _jumpForce = 16f;
    [SerializeField] private float _jumpForceAug = 16f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _jumpBufferCounter;
    [SerializeField] private bool _isJumping;
    [SerializeField] private float _jumpCooldown = 0.4f; 
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

    private void Jump_performed(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    private void Jump_canceled(InputAction.CallbackContext context)
    {
        if (PhysicsManager.Instance.IsGrounded == true)
        {
            Debug.Log("Salto1");
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            GameManager.GenerateSound(jump_sound);
        }
    }

}
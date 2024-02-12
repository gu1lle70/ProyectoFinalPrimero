using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JUMP : MonoBehaviour
{
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float _jumpForce = 16f;
    [SerializeField] private float _jumpMultiplier = 0.5f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _jumpBufferCounter;
    [SerializeField] private bool _isJumping;
    [SerializeField] private float _jumpCooldown = 0.4f; 
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        if (PhysicsManager.Instance.IsGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.fixedDeltaTime;
        }

        if (coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && !_isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);

            _jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }
        if (context.started && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * _jumpMultiplier);

            coyoteTimeCounter = 0f;
        }
        
    }
    private IEnumerator JumpCooldown()
    {
        _isJumping = true;
        yield return new WaitForSeconds(_jumpCooldown);
        _isJumping = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JUMP : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 50;

    public Rigidbody2D rb;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && PlayerMove.Instance._isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }
    }

}

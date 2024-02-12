using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJump : MonoBehaviour
{
    private RaycastHit2D right_hit;
    private RaycastHit2D left_hit;
    [SerializeField] private float range;
    [SerializeField] private float jump_force;
    [SerializeField] private LayerMask wall_mask;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (PhysicsManager.Instance.IsGrounded)
            return;

        right_hit = Physics2D.Raycast(transform.position, transform.right, range, wall_mask);
        left_hit = Physics2D.Raycast(transform.position, -transform.right, range, wall_mask);
    }
    public void Wall_Jump(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (right_hit)
            {
                Debug.Log("Contact");

                rb.velocity = new Vector2(0, 0);
                rb.AddForce((-transform.right + transform.up) * jump_force, ForceMode2D.Impulse);
            }
            if (left_hit)
            {
                Debug.Log("Contact");

                rb.velocity = new Vector2(0, 0);
                rb.AddForce((transform.right + transform.up) * jump_force, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

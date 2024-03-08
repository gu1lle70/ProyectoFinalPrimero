using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class WallJump : MonoBehaviour
{
    public static WallJump instance { get; private set; }

    public bool onWallJump = false;

    private RaycastHit2D right_hit;
    private RaycastHit2D left_hit;
    [SerializeField] private float range;
    [SerializeField] private float jump_force;
    [SerializeField] private float slide_speed;
    [SerializeField] private LayerMask wall_mask;

    private PlayerInput pl_input;

    private Rigidbody2D rb;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        rb = GetComponent<Rigidbody2D>();

        pl_input = new PlayerInput();
        pl_input.Player.Enable();

        pl_input.Player.Jump.performed += Wall_Jump;
    }

    private void FixedUpdate()
    {
        if (PhysicsManager.Instance.IsGrounded)
            return;
        Debug.Log("Casted");
        right_hit = Physics2D.Raycast(transform.position, transform.right, range, wall_mask);
        left_hit = Physics2D.Raycast(transform.position, -transform.right, range, wall_mask);

        if ((right_hit || left_hit) && rb.velocity.y <= 0 && PlayerMove.Instance._dir.y >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, slide_speed);
        }
    }
    public void Wall_Jump(InputAction.CallbackContext con)
    {
        if (right_hit)
        {
            Debug.Log("Contact");

            rb.velocity = new Vector2(-jump_force * 0.7f, jump_force);
            //rb.AddForce((-transform.right + transform.up) * jump_force, ForceMode2D.Impulse);

            onWallJump = true;
            StartCoroutine(WallDelay());
        }
        if (left_hit)
        {
            Debug.Log("Contact");

            rb.velocity = new Vector2(jump_force * 0.7f, jump_force);
            //rb.AddForce((transform.right + transform.up) * jump_force, ForceMode2D.Impulse);

            onWallJump = true;
            StartCoroutine(WallDelay());
        }
    }

    private IEnumerator WallDelay()
    {
        yield return new WaitForSeconds(0.3f);
        onWallJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

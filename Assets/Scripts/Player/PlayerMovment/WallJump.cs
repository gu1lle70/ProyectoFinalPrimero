using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJump : MonoBehaviour
{
    public static WallJump instance { get; private set; }

    public bool onWallJump = false;
    public bool sliding = false;

    private RaycastHit2D right_hit;
    private RaycastHit2D left_hit;
    [SerializeField] private float range = 0.5f;
    [SerializeField] private float jump_force = 10f;
    [SerializeField] private float slide_speed = -2f;
    [SerializeField] private LayerMask wall_mask;
    [SerializeField] private AudioClip slide_clip;
    [SerializeField] private AudioClip jump_clip;

    private PlayerInput pl_input;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        pl_input = new PlayerInput();
        pl_input.Player.Enable();

        pl_input.Player.Jump.performed += Wall_Jump;
    }

    private void FixedUpdate()
    {
        if (PhysicsManager.Instance.IsGrounded)
            return;

        // Detect walls to the left and right
        right_hit = Physics2D.Raycast(transform.position, Vector2.right, range, wall_mask);
        left_hit = Physics2D.Raycast(transform.position, Vector2.left, range, wall_mask);

        if (!right_hit && !left_hit)
        {
            sliding = false;
        }
        else if ((right_hit || left_hit) && rb.velocity.y <= 0 && PlayerMove.Instance._dir.y >= 0)
        {
            // Determine the direction the player is facing
            if (right_hit)
                PlayerSprites.Instance.spriteRenderer.flipX = false;
            else
                PlayerSprites.Instance.spriteRenderer.flipX = true;

            // Apply slide speed
            rb.velocity = new Vector2(rb.velocity.x, slide_speed);
            sliding = true;

            if (!audioSource.isPlaying && rb.velocity.y < -0.15f)
            {
                audioSource.clip = slide_clip;
                audioSource.Play();
            }
        }
    }

    private void Wall_Jump(InputAction.CallbackContext context)
    {
        if (right_hit)
        {
            PerformWallJump(-1);
        }
        else if (left_hit)
        {
            PerformWallJump(1);
        }
    }

    private void PerformWallJump(int direction)
    {
        Debug.Log("Wall contact");

        // Apply jump force
        rb.velocity = new Vector2(direction * jump_force * 0.7f, jump_force);
        StartCoroutine(WallDelay());

        GameManager.Instance.GenerateSound(jump_clip);
    }

    private IEnumerator WallDelay()
    {
        sliding = false;
        onWallJump = true;

        audioSource.Stop();
        yield return new WaitForSeconds(0.25f);
        onWallJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
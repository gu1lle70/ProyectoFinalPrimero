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
    [SerializeField] private float range;
    [SerializeField] private float jump_force;
    [SerializeField] private float slide_speed;
    [SerializeField] private LayerMask wall_mask;
    [SerializeField] private AudioClip slide_clip;
    [SerializeField] private AudioClip jump_clip;

    private PlayerInput pl_input;
    private Rigidbody2D rb;
    private float gravityScale;
    private AudioSource audioSource;
    private DASH dashScript;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;

        audioSource = GetComponent<AudioSource>();

        pl_input = new PlayerInput();
        pl_input.Player.Enable();

        pl_input.Player.Jump.performed += Wall_Jump;

        dashScript = DASH.instance;
    }

    private void FixedUpdate()
    {
        if (PhysicsManager.Instance.IsGrounded || PlayerMove.Instance._dir.y < 0)
        {
            sliding = false;
            onWallJump = false;
            rb.gravityScale = gravityScale;
            return;
        }

        right_hit = Physics2D.Raycast(transform.position, transform.right, range, wall_mask);
        left_hit = Physics2D.Raycast(transform.position, -transform.right, range, wall_mask);

        sliding = right_hit || left_hit;

        if (sliding && !onWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, -slide_speed);
            rb.gravityScale = 0.2f;
            if (!audioSource.isPlaying && rb.velocity.y < -0.15f)
            {
                audioSource.clip = slide_clip;
                audioSource.Play();
            }
        }
        else
        {
            rb.gravityScale = gravityScale;
            audioSource.Stop();
        }

        if (right_hit)
        {
            if (right_hit.distance < 0.25f)
                transform.Translate(Vector2.left * 0.1f);
            PlayerSprites.Instance.spriteRenderer.flipX = false;
        }
        else if (left_hit)
        {
            if (left_hit.distance < 0.25f)
                transform.Translate(Vector2.right * 0.1f);
            PlayerSprites.Instance.spriteRenderer.flipX = true;
        }
    }

    public void Wall_Jump(InputAction.CallbackContext con)
    {
        if (right_hit || left_hit)
        {
            Vector2 jumpDirection = new Vector2(left_hit ? jump_force * 0.7f : -jump_force * 0.7f, jump_force);
            rb.velocity = jumpDirection;

            StartCoroutine(WallDelay());
        }
    }

    private IEnumerator WallDelay()
    {
        sliding = false;
        onWallJump = true;
        rb.gravityScale = gravityScale;
        GameManager.Instance.GenerateSound(jump_clip);
        audioSource.Stop();
        yield return new WaitForSeconds(0.25f);

        onWallJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * range);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * range);
    }
}
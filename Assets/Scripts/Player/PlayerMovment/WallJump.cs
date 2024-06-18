using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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
    }

    private void FixedUpdate()
    {
        if (PhysicsManager.Instance.IsGrounded || PlayerMove.Instance._dir.y < 0)
        {
            sliding = false;
            onWallJump = false;
            return;
        }

        right_hit = Physics2D.Raycast(transform.position, transform.right, range, wall_mask);
        left_hit = Physics2D.Raycast(transform.position, -transform.right, range, wall_mask);

        if (!right_hit || !left_hit)
            sliding = false;

        if (right_hit || left_hit && PlayerMove.Instance._dir.y >= 0 && !onWallJump)
        {
            rb.velocity = new Vector2(0, slide_speed * Time.deltaTime);
            rb.gravityScale = 0.2f;
            sliding = true;
        }

        if (right_hit)
        {
            if (right_hit.distance > 0.41f)
                rb.velocity = new Vector2(PlayerMove.Instance._dir.x * -slide_speed * 2, rb.velocity.y);
            Debug.Log(right_hit.distance);

            PlayerSprites.Instance.spriteRenderer.flipX = false;
        }
        if (left_hit)
        {
            if (left_hit.distance > 0.41f)
                rb.velocity = new Vector2(PlayerMove.Instance._dir.x * -slide_speed * 2, rb.velocity.y);
            Debug.Log(left_hit.distance);
            PlayerSprites.Instance.spriteRenderer.flipX = true;
        }

        /*
        if (!right_hit || !left_hit)
        {
            sliding = false;
            return;
        }
        
        if ((right_hit || left_hit) && rb.velocity.y <= 0 && PlayerMove.Instance._dir.y >= 0)
        {
            if (right_hit)
                PlayerSprites.Instance.spriteRenderer.flipX = false;
            else
                PlayerSprites.Instance.spriteRenderer.flipX = true;

            rb.velocity = new Vector2(0, slide_speed);
            sliding = true;

        }
        else if (sliding)
            sliding = false;
        */
        if (!audioSource.isPlaying && (right_hit || left_hit) && rb.velocity.y < -0.15f)
        {
            audioSource.clip = slide_clip;
            audioSource.Play();
        }
    }
    public void Wall_Jump(InputAction.CallbackContext con)
    {
        if (right_hit)
        {
            Debug.Log("Wall contact");

            rb.velocity = new Vector2(-jump_force, jump_force);

            StartCoroutine(WallDelay());
        }
        if (left_hit)
        {
            Debug.Log("Wall contact");

            rb.velocity = new Vector2(jump_force, jump_force);

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
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

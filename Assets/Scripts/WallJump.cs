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
        
        right_hit = Physics2D.Raycast(transform.position, transform.right, range, wall_mask);
        left_hit = Physics2D.Raycast(transform.position, -transform.right, range, wall_mask);

        if ((right_hit || left_hit) && rb.velocity.y <= 0 && PlayerMove.Instance._dir.y >= 0)
        {
            if (right_hit)
                PlayerSprites.Instance.spriteRenderer.flipX = false;
            else
                PlayerSprites.Instance.spriteRenderer.flipX = true;

            rb.velocity = new Vector2(rb.velocity.x, slide_speed);
            sliding = true;

        }
        else if (sliding)
            sliding = false;

        if (!audioSource.isPlaying && (right_hit || left_hit) && rb.velocity.y < 0)
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

            rb.velocity = new Vector2(-jump_force * 0.7f, jump_force);

            StartCoroutine(WallDelay());
        }
        if (left_hit)
        {
            Debug.Log("Wall contact");

            rb.velocity = new Vector2(jump_force * 0.7f, jump_force);

            StartCoroutine(WallDelay());
        }
    }

    private IEnumerator WallDelay()
    {
        sliding = false;
        onWallJump = true;

        GameManager.GenerateSound(jump_clip);
        audioSource.Stop();
        yield return new WaitForSeconds(0.25f);
        onWallJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

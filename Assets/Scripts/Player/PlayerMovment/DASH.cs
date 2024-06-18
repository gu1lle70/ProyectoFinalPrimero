using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DASH : MonoBehaviour
{
    public static DASH instance { get; private set; }

    [Header("Sound")]
    [SerializeField] private AudioClip dash_sound;

    [SerializeField] public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GhostController ghostController;

    public bool canDash = true;
    public bool onCooldown = false;
    public bool isDashing;
    [SerializeField] public float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    public float superDashMultiplier = 2f;

    public int dash_num = 1;

    private void Start()
    {
        ghostController.enabled = false;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (isDashing)
        {
            rb.gravityScale = 0f;
            return;
        }
        else
        {
            rb.gravityScale = 1.2f;
        }

        if (PhysicsManager.Instance.IsGrounded || WallJump.instance.sliding)
        {
            canDash = true;
            if (dash_num <= 0)
            {
                dash_num = 1;
                FollowScript.instance.currentOrbs = dash_num;
            }
            onCooldown = false;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !onCooldown && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        if (dash_num <= 1)
            onCooldown = true;

        Vector2 dir = GetDashDirection();

        GameManager.Instance.GenerateSound(dash_sound);
        ghostController.enabled = true;

        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));



        dash_num--;
        FollowScript.instance.currentOrbs = dash_num;

        if (dash_num <= 0)
            canDash = false;

        isDashing = true;
        rb.velocity = dir * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        rb.velocity = Vector2.zero;
        ghostController.enabled = false;

        if (!PhysicsManager.Instance.IsGrounded)
        {
            yield return new WaitForSeconds(dashingCooldown - dashingTime);
        }
        onCooldown = false;
    }

    public Vector2 GetDashDirection()
    {
        Vector2 dir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) dir.y += 1;
        if (Keyboard.current.sKey.isPressed) dir.y -= 1;
        if (Keyboard.current.dKey.isPressed) dir.x += 1;
        if (Keyboard.current.aKey.isPressed) dir.x -= 1;

        if (dir == Vector2.zero) dir.x = PlayerSprites.Instance.facingDirection;

        return dir.normalized;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance { get; private set; }

    public Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] public float _speed = 10;
    [SerializeField] private float _slow_multiplier = 0.9f;
    [SerializeField] private LayerMask ground_layer;
    [Space]
    [Header("Acceleration and Deceleration")]
    [SerializeField] public float _acceleration = 2f;
    [SerializeField] public float _deceleration = 3f;
    [SerializeField] public float _maxSpeed = 10f;
    [Space]
    [Header("Inputs")]
    [SerializeField] public float _horizontal = 1f;
    [SerializeField] public Vector2 _dir;
    

    public bool isNotInTutorial = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (WallJump.instance.sliding || WallJump.instance.onWallJump)
            return;

        RaycastHit2D ray_to_ground = Physics2D.Raycast(transform.position, Vector2.down, ground_layer);
        if (ray_to_ground && Vector2.Distance(transform.position, ray_to_ground.transform.position) > 100 && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -1);
        }

        if (!DASH.instance.isDashing && !WallJump.instance.onWallJump)
        {
            float targetSpeed = _horizontal * _speed;

            if (_horizontal != 0)
            {
                // Aceleración
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, targetSpeed, _acceleration * Time.fixedDeltaTime), rb.velocity.y);
            }
            else if (_horizontal == 0 && PhysicsManager.Instance.IsGrounded)
            {
                // Desaceleración
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, _deceleration * Time.fixedDeltaTime), rb.velocity.y);
            }

            // Límite de velocidad
            if (Mathf.Abs(rb.velocity.x) > _maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * _maxSpeed, rb.velocity.y);
            }

            if (rb.velocity.magnitude > 0 && _horizontal == 0 && PhysicsManager.Instance.IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x * _slow_multiplier, rb.velocity.y);
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (isNotInTutorial)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _horizontal = Mathf.RoundToInt(input.x);
            _dir = new Vector2(_horizontal, 0);
        }
    }
}
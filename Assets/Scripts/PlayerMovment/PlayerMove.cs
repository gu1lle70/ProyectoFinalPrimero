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
    [SerializeField] private float _slow_multiplier; // Este se usa --- !! ---
    [SerializeField] private LayerMask ground_layer; // Este se usa --- !! ---
    [Space]
    [Header("Inputs")]
    [SerializeField] public float _horizontal = 1f;
    [SerializeField] public float _vertical;
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
        RaycastHit2D ray_to_ground = Physics2D.Raycast(transform.position, Vector2.down, ground_layer);
        if (ray_to_ground && Vector2.Distance(transform.position, ray_to_ground.transform.position) > 100 && rb.velocity.y > 0) // El jugador está muy lejos del suelo y sigue subiendo
        {
            rb.velocity = new Vector2(rb.velocity.x, -1);
        }

        //Debug.Log(Vector2.Distance(transform.position, ray_to_ground.transform.position));

        if (!DASH.instance.isDashing && !WallJump.instance.onWallJump)
        {
            rb.velocity = new Vector2((_horizontal * _speed * (Time.deltaTime + 1) + rb.velocity.x), rb.velocity.y);

            if (rb.velocity.magnitude > _speed * 1.5f) // Límite de velocidad
            {
                rb.velocity = new Vector2(_horizontal * _speed, rb.velocity.y);
            }
            if (rb.velocity.magnitude > 0 && _dir.x == 0 && PhysicsManager.Instance.IsGrounded) // slow cuando dejas de moverte estando en el suelo
            {
                rb.velocity = new Vector2(rb.velocity.x * _slow_multiplier, rb.velocity.y);
            }
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        if (isNotInTutorial)
        {
            _horizontal = context.ReadValue<Vector2>().x;
            _vertical = context.ReadValue<Vector2>().y;
            _dir = new Vector2(_horizontal, _vertical);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance { get; private set; }

    public Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _slideSpeed = 5;
    [SerializeField] private float _wallJumpLerp = 10;
    [SerializeField] private float _dashSpeed = 20;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [Space]
    [Header("Booleans")]
    [SerializeField] private bool _canMove;
    [SerializeField] private bool _wallGrab;
    [SerializeField] private bool _wallJumped;
    [SerializeField] private bool _wallSlide;
    [SerializeField] private bool _isDashing;
    [SerializeField] private bool _hasDashed;
    [Space]
    [Header("Inputs")]
    [SerializeField] public float _horizontal = 1f;
    [SerializeField] public float _vertical;
    [SerializeField] public Vector2 _dir;


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
         rb.velocity = new Vector2(_horizontal * _speed, rb.velocity.y);
    }


    public void Move(InputAction.CallbackContext context)
    {

        _horizontal = context.ReadValue<Vector2>().x;
        _vertical = context.ReadValue<Vector2>().y;
        _dir = new Vector2(_horizontal, _vertical);
    }
   
  
}

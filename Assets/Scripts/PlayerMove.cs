using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;

    [Space]
    [Header("Stats")]
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _jumpForce = 50;
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
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _hasDashed;
    [Space]

    public int side = 1;
    [Header("Inputs")]
    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;
    [SerializeField] private Vector2 _dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _dir = new Vector2(_horizontal, _vertical);
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(_horizontal * _speed, rb.velocity.y)), _wallJumpLerp * Time.deltaTime);
    }


    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
        _vertical = context.ReadValue<Vector2>().y;
    }
    
}

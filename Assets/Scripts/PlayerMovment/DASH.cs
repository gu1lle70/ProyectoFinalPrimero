using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DASH : MonoBehaviour
{
    public static DASH instance { get; private set; }

    [Header("Sound")]
    [SerializeField] private AudioClip dash_sound;
    [Header("TrailRenderer")]
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] private Rigidbody2D rb;


    public bool canDash = true;
    public bool onCooldown = false;
    public bool isDashing;
    [SerializeField]private float dashingPower = 24f;
    [SerializeField]private float dashingTime = 0.2f;
    [SerializeField]private float dashingCooldown = 1f;

    public int dash_num = 1;

   

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
            return;
        }
        if (PhysicsManager.Instance.IsGrounded || WallJump.instance.sliding)
        {
            canDash = true;
            if (dash_num <= 0)
            {
                dash_num = 1;
                FollowScript.instance.currentOrbs = dash_num;
            }
        }

    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !onCooldown && canDash)
        {
            StartCoroutine(Dash());
        }

    }
private IEnumerator Dash()
    {
        if (dash_num <= 1)
            onCooldown = true;

        Vector2 dir = PlayerMove.Instance._dir;

        GameManager.GenerateSound(dash_sound);
        trailRenderer.emitting = true;

        dash_num--;
        FollowScript.instance.currentOrbs = dash_num;

        if (dash_num <= 0)
            canDash = false;

        isDashing = true;
        if (dir.x != 0)
        {
            rb.velocity = new Vector2(dir.x * dashingPower, dir.y * dashingPower);
        }
        else
        {
            if (!PhysicsManager.Instance.IsGrounded && dir.y == -1)
                rb.velocity = new Vector2(0, dir.y * dashingPower);
            else
                rb.velocity = new Vector2(dashingPower * PlayerSprites.Instance.facingDirection, dir.y * dashingPower);
        }
        /*
        if (PlayerSprites.Instance.spriteRenderer.flipX == false)
        {
            //rb.velocity = new Vector2(transform.localScale.x * dashingPower, PlayerMove.Instance._dir.y * dashingPower);
            rb.AddForce(PlayerMove.Instance._dir *  dashingPower, ForceMode2D.Impulse);
        }
        else
        {
            //rb.velocity = new Vector2(transform.localScale.y * -dashingPower, PlayerMove.Instance._dir.y * -dashingPower);
            rb.AddForce(PlayerMove.Instance._dir * -dashingPower, ForceMode2D.Impulse);
        }*/




        yield return new WaitForSeconds(dashingTime);
       
        isDashing = false;

        rb.velocity = Vector2.zero;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashingCooldown - dashingTime);
        onCooldown = false;
    }
}

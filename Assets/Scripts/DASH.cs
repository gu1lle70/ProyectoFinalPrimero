using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DASH : MonoBehaviour
{
    public static DASH instance { get; private set; }

    [SerializeField]private bool canDash = true;
    private bool onCooldown = false;
    public bool isDashing;
    [SerializeField]private float dashingPower = 24f;
    [SerializeField]private float dashingTime = 0.2f;
    [SerializeField]private float dashingCooldown = 1f;

    public int dash_num = 1;

    [SerializeField] private Rigidbody2D rb;

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
        if (PhysicsManager.Instance.IsGrounded)
        {
            canDash = true;
            if (dash_num <= 0)
                dash_num = 1;
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

        dash_num--;
        if (dash_num <= 0)
            canDash = false;

        isDashing = true;

        if(PlayerSprites.Instance.spriteRenderer.flipX == false)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else
        {
            rb.velocity -= new Vector2(transform.localScale.y * dashingPower, 0f);
        }
        
        
        yield return new WaitForSeconds(dashingTime);
       
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown - dashingTime);
        onCooldown = false;
    }
}

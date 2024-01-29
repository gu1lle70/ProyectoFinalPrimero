using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASH : MonoBehaviour
{
    [SerializeField]private bool canDash = true;
    private bool isDashing;
    [SerializeField]private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && canDash)
        {
            StartCoroutine(Dash());
        }

        if(PlayerMove.Instance._isGrounded == true)
        {
            canDash = true;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if(CameraController.Instance.der == true)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else if (CameraController.Instance.izq == true)
        {
            rb.velocity -= new Vector2(transform.localScale.y * dashingPower, 0f);
        }
        
        
        yield return new WaitForSeconds(dashingTime);
       
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance { get; private set; }

    [Header("Grounded")]
    [SerializeField] public bool IsGrounded;
    [SerializeField] private LayerMask groundedLayer;
    [SerializeField] private Transform _groundPos;
    [SerializeField] private float _radius = 0.2f;
    [Header("Leadge")]
    [SerializeField] public Transform ledgeCheck;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public float ledgeDistance = 10.0f;
    public Vector2 ledgePosBot;
    public Vector2 ledgePos1;
    public Vector2 ledgePos2;
  
    public bool isTouchingLedge;
    public bool isTouchingWall;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(_groundPos.position, _radius, groundedLayer);
        IsGrounded = hit != null;

        RaycastHit2D wallHit = Physics2D.Raycast(wallCheck.position, transform.right, ledgeDistance, groundedLayer);
        isTouchingWall = wallHit.collider != null;

        RaycastHit2D ledgeHit = Physics2D.Raycast(ledgeCheck.position, transform.right, ledgeDistance, groundedLayer);
        isTouchingLedge = ledgeHit.collider != null;

        //Debug.Log("Hit: " + hit);
        if(isTouchingWall && !isTouchingLedge && !LeadgeClimb.Instance.ledgeDetected)
        {
            LeadgeClimb.Instance.ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }


    void OnDrawGizmosSelected()
    {
        if (IsGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(_groundPos.position, _radius);
        Gizmos.DrawRay(ledgeCheck.position, transform.right * ledgeDistance);
        Gizmos.DrawRay(wallCheck.position, transform.right * ledgeDistance);
    }
}
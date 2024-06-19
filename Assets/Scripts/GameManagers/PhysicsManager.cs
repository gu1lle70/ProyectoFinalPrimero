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
    }
}
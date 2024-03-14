using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public static FollowScript instance { get; private set; }

    [SerializeField] private Transform reference;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float spacing;

    [SerializeField] private List<Transform> orbs;
    private int lastInLine;
    public int currentOrbs;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        orbs = new List<Transform>
        {
            transform
        };

        for (int i = 0; i < 5; i++)
        {
            GameObject ob = Instantiate(this.gameObject, transform.parent);
            orbs.Add(ob.transform);
            ob.transform.position = Vector3.zero;
        }
    }
    void FixedUpdate()
    {

        for (int i = 0; i < currentOrbs; i++)
        {
            Vector2 pos = new Vector2(reference.position.x + ((offset.x * i) - i) * PlayerSprites.Instance.facingDirection, reference.position.y + offset.y);
            //Vector2 pos = new Vector2(reference.position.x + (offset.x * (i / (0.5f + spacing)) * PlayerSprites.Instance.facingDirection), reference.position.y + offset.y);
            float distance = Vector2.Distance(transform.position, pos);
            orbs[i].position = Vector2.Lerp(transform.position, pos, distance / 50);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = new Vector2(reference.position.x + offset.x, reference.position.y + offset.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pos, transform.localScale.x);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public static FollowScript instance { get; private set; }

    [SerializeField] private Transform reference;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float spacing;

    [SerializeField] private int orbsToGenerate;
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

        for (int i = 0; i < orbsToGenerate; i++)
        {
            GameObject ob = Instantiate(this.gameObject, transform.parent);
            orbs.Add(ob.transform);
            ob.transform.position = new Vector3(1000, 1000, 0);
        }

        offset.z = 0;
    }
    void FixedUpdate()
    {
        for (int i = 0; i < currentOrbs; i++)
        {
            Vector2 pos;
            int dir = PlayerSprites.Instance.facingDirection;
            float distance;
            if (i == 0)
            {
                pos = new Vector2(reference.position.x + (offset.x * dir), reference.position.y + offset.y);
                distance = Vector2.Distance(orbs[i].position, pos);
            }
            else
            {
                pos = new Vector3(
                        orbs[i - 1].position.x - (spacing * dir),
                        reference.position.y + offset.y, 0
                    );
                distance = Vector2.Distance(orbs[i].position, orbs[i - 1].position);
            }

            orbs[i].position = Vector2.Lerp(orbs[i].position, pos, distance * (Time.deltaTime + 0.1f));
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = new Vector2(reference.position.x + offset.x, reference.position.y + offset.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pos, transform.localScale.x);
    }
}
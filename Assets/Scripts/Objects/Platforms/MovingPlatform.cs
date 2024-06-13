using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int current;
    [SerializeField] private List<Vector2> points;

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, points[current], speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[current]) < 0.1f)
        {
            if (current < points.Count - 1)
                current++;
            else
                current = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i <  points.Count; i++)
        {
            Gizmos.DrawWireCube(points[i], Vector2.one);
        }
    }
}
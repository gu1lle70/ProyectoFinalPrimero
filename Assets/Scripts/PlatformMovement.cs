using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions;

    private int index;

    private bool isActive = false;

    // Update is called once per frame
    void Update()
    {      
            transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

            if (transform.position == positions[index])
            {
                if (index == positions.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }    
}




/*
 public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions;

    private int index;

    private bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

            if (transform.position == positions[index])
            {
                if (index == positions.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }
    }

    public void ActivatePlatform()
    {
        isActive = true;
    }
}
 */
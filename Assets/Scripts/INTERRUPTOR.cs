using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTERRUPTOR : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}

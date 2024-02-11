using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float life_time;
    [SerializeField] private float bulletSpeed;

    private float direction = 0;
    
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (PlayerSprites.Instance.spriteRenderer.flipX)
            direction = -1;
        else
            direction = 1;
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        rb.AddForce(transform.right * direction * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
        yield return new WaitForSeconds(life_time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
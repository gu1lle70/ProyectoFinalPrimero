using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLike : MonoBehaviour
{
    [SerializeField] private float waterNum = 0.6f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Time.timeScale = waterNum;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Time.timeScale = 1.0f;
    }
}

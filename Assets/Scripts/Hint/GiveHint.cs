using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHint : MonoBehaviour
{
    [SerializeField] GameObject hintPrefab;
    private void Start()
    {
        DASH.instance.enabled = false;
        DASH.instance.canDash = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DASH.instance.enabled = true;
        DASH.instance.canDash = true;
        Time.timeScale = 0.1f;
        hintPrefab.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Time.timeScale = 1.0f;
        hintPrefab.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

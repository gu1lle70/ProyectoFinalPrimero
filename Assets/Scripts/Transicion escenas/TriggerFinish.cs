using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinish : MonoBehaviour
{
    public static TriggerFinish Instance;
    public bool finishTutorial = false;

    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("empieza final tutorial");
            finishTutorial = true;
        }
    }
}

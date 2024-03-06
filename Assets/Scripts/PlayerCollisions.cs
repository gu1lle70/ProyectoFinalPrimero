using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class PlayerCollisions : MonoBehaviour
{
    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Pinchos")
        {
            SceneManager.LoadScene("Menu"); // Muerte, cambiar a restar vidas etc. aquí según hagamos
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer black_bg;

    private bool start_fade;

    private void Update()
    {
        if (!start_fade)
            return;

        if (black_bg.color.a < 255)
            black_bg.color = new Color(black_bg.color.r, black_bg.color.g, black_bg.color.b, black_bg.color.a + Time.deltaTime);
        else
            SceneManager.LoadScene("ROGER_ZONA_PRUEBAS");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            start_fade = true;
        }
    }
}
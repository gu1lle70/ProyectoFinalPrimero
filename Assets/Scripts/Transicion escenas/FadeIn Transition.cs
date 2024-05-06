using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInTransition : MonoBehaviour
{
    [SerializeField] private SpriteRenderer black_bg;

    private bool start_fade;
    private float fadeSpeed = 0.5f; // Ajusta este valor para controlar la velocidad del fade in

    private void Update()
    {
        if (!start_fade)
            return;

        Color color = black_bg.color;
        if (color.a > 0f) // Verifica si el canal alfa es mayor que cero
        {
            color.a -= fadeSpeed * Time.deltaTime; // Disminuye gradualmente el canal alfa
            black_bg.color = color;
        }
        else
        {
            SceneManager.LoadScene("ROGER_ZONA_PRUEBAS");
            start_fade = false; // Restablece start_fade para evitar un fade in continuo
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !start_fade) // Verifica si el fade in no ha comenzado ya
        {
            start_fade = true;
        }
    }

}

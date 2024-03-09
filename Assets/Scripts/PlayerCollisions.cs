using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private float grabCooldown;
    private bool itemGrabbed = false;

    // Tiene que haber un collider en trigger para que funcione
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Spikes")
        {
            Debug.Log("Muerte");
            //SceneManager.LoadScene("Menu"); // cambiar a restar vidas etc. aquí según hagamos
        }
        else if (coll.tag == "Dash orb" && !itemGrabbed)
        {
            DASH.instance.dash_num++;
            Destroy(coll.gameObject); // Si hay que optimizar se puede cambiar por un setActive a false

            itemGrabbed = true;
            StartCoroutine(GrabCooldown());
        }
    }

    private IEnumerator GrabCooldown()
    {
        yield return new WaitForSeconds(grabCooldown);
        itemGrabbed = false;
    }
}
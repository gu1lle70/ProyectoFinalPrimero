using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{

    private GameObject teleportZone;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (teleportZone != null)
            {
                transform.position = teleportZone.GetComponent<Teleport>().GetDestination().position;
            }
        }
    }

    private void OntriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            teleportZone = collision.gameObject;
        }
    }


    private void OntriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == teleportZone)
            {
                teleportZone = null;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private float grabCooldown;
    [SerializeField] private float orbCooldown;
    [SerializeField] private bool canPickOrb = true;
    
    private bool itemGrabbed = false;

    // Tiene que haber un collider en trigger para que funcione
    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.tag == "Dash orb" && !itemGrabbed && canPickOrb)
        {
            StartCoroutine(GrabOrb(coll));
        }
        else if (coll.tag == "Checkpoint" && !itemGrabbed)
        {
            CheckPoints.Checkpoint_manager.currentCheckpoint = coll.transform;
            coll.GetComponent<CheckPoint>().cameraPlaces.ChangeCameraPlace();
            StartCoroutine(GrabCooldown());
        }
        else if (coll.tag == "MovingPlatform")
        {
            transform.parent = coll.transform;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (transform.parent != null)
            transform.parent = null;
    }

    private IEnumerator GrabCooldown()
    {
        itemGrabbed = true;
        yield return new WaitForSeconds(grabCooldown);
        itemGrabbed = false;
    }

    private IEnumerator GrabOrb(Collider2D coll)
    {
        canPickOrb = false;
        DASH.instance.dash_num++;
        DASH.instance.canDash = true;
        DASH.instance.onCooldown = false;
        FollowScript.instance.currentOrbs++;
        coll.gameObject.GetComponentInChildren<Light2D>().enabled = false;
        StartCoroutine(GrabCooldown());
        yield return new WaitForSeconds(orbCooldown);
        coll.gameObject.GetComponentInChildren<Light2D>().enabled = true;
        canPickOrb = true;
    }

}
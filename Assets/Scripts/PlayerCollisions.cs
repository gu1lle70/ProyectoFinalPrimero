using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            Debug.Log("Death");
            CheckPoints.Checkpoint_manager.ReturnToCheckpoint();
        }
        else if (coll.tag == "Dash orb" && !itemGrabbed)
        {
            DASH.instance.dash_num++;
            DASH.instance.canDash = true;
            DASH.instance.onCooldown = false;
            FollowScript.instance.currentOrbs++;
            coll.gameObject.SetActive(false); // Si hay que optimizar se puede cambiar por un setActive a false

            StartCoroutine(GrabCooldown());
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
}
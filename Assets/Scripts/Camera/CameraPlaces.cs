using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaces : MonoBehaviour
{
    public float cameraZoom;
    public Transform position;
    public CameraController.CameraMode cameraMode;
    public bool canSpawnAnotherCamera;
    public GameObject secondPlace;


    private void Start()
    {
        position = transform;
    }
    private void OnDrawGizmos()
    {
        Vector3 cameraSize = new Vector3(cameraZoom * 2 - 3f, cameraZoom, cameraZoom);
        Gizmos.DrawWireCube(transform.position, cameraSize);
    }

    public void ChangeCameraPlace()
    {
        position = transform;
        CameraController.Instance.ChangePosition(this);
        if (canSpawnAnotherCamera)
            secondPlace.SetActive(true);
    }
}
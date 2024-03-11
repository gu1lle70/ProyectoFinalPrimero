using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaces : MonoBehaviour
{
    public int cameraZoom;
    [HideInInspector] public Transform position;
    public CameraController.CameraMode cameraMode;


    private void Start()
    {
        position = transform;
    }
    private void OnDrawGizmos()
    {
        Vector3 cameraSize = new Vector3(cameraZoom * 2 - 2.2f, cameraZoom, cameraZoom);
        Gizmos.DrawWireCube(transform.position, cameraSize);
    }
}
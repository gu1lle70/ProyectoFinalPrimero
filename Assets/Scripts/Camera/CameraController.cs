using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [HideInInspector] public enum CameraMode { STATIC, DYNAMIC, LAST_NO_USE }
    [Header("Camera mode")]
    public CameraMode cameraMode;

    [Header("References")]
    public GameObject player;
    [Header("Stats")]
    public float offset = 2;
    public float offsetSmoothing = 5;
    private Vector3 playerPosition;
    public bool der;
    public bool izq;

    public bool moveToNextPosition;
    [SerializeField]private CameraPlaces cameraPosition;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    void Start()
    {
        der = true;
        moveToNextPosition = true;
    }

    void Update()
    {
        switch (cameraMode)
        {
            case CameraMode.DYNAMIC: // Cámara dinámica -----------------------------------------------------------------------------------------
                playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

                if (PlayerMove.Instance._dir.x >= 0 && der == true)
                {
                    playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
                }
                else if (PlayerMove.Instance._dir.x <= 0 && izq == true)
                {
                    playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
                }
                transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);

                if (PlayerMove.Instance._dir.x == 1)
                {
                    der = true;
                    izq = false;
                }
                else if (PlayerMove.Instance._dir.x == -1)
                {
                    der = false;
                    izq = true;
                }
                break;
            case CameraMode.STATIC: // Cámara estática -----------------------------------------------------------------------------------------
                if (moveToNextPosition)
                {
                    transform.position = Vector3.Lerp(transform.position, cameraPosition.position.position, offsetSmoothing * Time.deltaTime);
                    if (Vector2.Distance(transform.position, cameraPosition.position.position) < 0.05f)
                    {
                        StartCoroutine(ChangeCameraSize(cameraPosition.cameraZoom / 2));

                        moveToNextPosition = false;
                    }
                }
                break;
        }
    }

    public void ChangePosition(CameraPlaces cp)
    {
        cameraPosition = cp;
        moveToNextPosition = true;
        cameraMode = cp.cameraMode;
    }

    private IEnumerator ChangeCameraSize(float objective)
    {
        float cameraSize = Camera.main.orthographicSize;

        if (cameraSize > objective)
            while (Camera.main.orthographicSize > objective)
            {
                Camera.main.orthographicSize -= 0.05f;
                yield return new WaitForSeconds(0.001f);
            }
        else
            while (Camera.main.orthographicSize < objective)
            {
                Camera.main.orthographicSize += 0.05f;
                yield return new WaitForSeconds(0.001f);
            }
    }
}
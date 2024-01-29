using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    public bool der;
    public bool izq;
    // Start is called before the first frame update
    void Start()
    {
        der = true;
    }

    // Update is called once per frame
    void Update()
    {
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

        if(PlayerMove.Instance._dir.x == 1)
        {
            der = true;
            izq = false;
        }
        else if (PlayerMove.Instance._dir.x == -1)
        {
            der = false; 
            izq = true;
        }


    }

}

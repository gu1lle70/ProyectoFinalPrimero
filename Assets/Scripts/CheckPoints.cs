using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public static CheckPoints Checkpoint_manager {  get; private set; }

    public Transform currentCheckpoint;

    [Header("Player")]
    [SerializeField] private Transform player;
    
    void Awake()
    {
        if (Checkpoint_manager == null)
            Checkpoint_manager = this;
        else
            Destroy(this);
    }

    public void ReturnToCheckpoint()
    {
        player.position = currentCheckpoint.position;
    }
}
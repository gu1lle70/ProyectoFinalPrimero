using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public static CheckPoints Checkpoint_manager {  get; private set; }

    public Transform currentCheckpoint;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Dash orbs parent")]
    [SerializeField] private GameObject dashOrbs_holder;
    
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
        for (int i = 0; i < dashOrbs_holder.transform.childCount; i++)
            dashOrbs_holder.transform.GetChild(i).gameObject.SetActive(true);
    }
}
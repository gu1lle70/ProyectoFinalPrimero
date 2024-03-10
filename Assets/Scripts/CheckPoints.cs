using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public static CheckPoints Checkpoint_manager {  get; private set; }

    [SerializeField] private List<CheckPoint> checkPoints;
    public int current_checkpoint = 0;

    [Header("Player")]
    [SerializeField] private Transform player;
    
    void Awake()
    {
        if (Checkpoint_manager == null)
            Checkpoint_manager = this;
        else
            Destroy(this);
    }

    public void ClaimCheckpoint()
    {
        if (current_checkpoint + 1 < checkPoints.Count - 1)
        {
            if (!checkPoints[current_checkpoint + 1].claimed)
            {
                current_checkpoint++;
                checkPoints[current_checkpoint].claimed = true;
            }
        }
    }
    public void ReturnToCheckpoint(int n)
    {
        if (n >= checkPoints.Count)
            return;
        player.position = checkPoints[n].pos.position;
        current_checkpoint = n;
    }
}
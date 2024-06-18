using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollision : MonoBehaviour
{
    [Header("DeathZones")]
    [SerializeField] public BoxCollider2D playerDeathZone;
    [SerializeField] private AudioClip death_sound;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Lava"))
        {
            Debug.Log("Death");
            GameManager.Instance.GenerateSound(death_sound);
            CheckPoints.Checkpoint_manager.ReturnToCheckpoint();
        }
    }
}

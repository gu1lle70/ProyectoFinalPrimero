using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip footStep_clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private void Update()
    {
        if (PlayerMove.Instance._dir.x == 0 && audioSource.isPlaying && !WallJump.instance.sliding)
            audioSource.Stop();
    }

    public void PlayFootStep()
    {
        audioSource.clip = footStep_clip;
        audioSource.Play();
    }
}

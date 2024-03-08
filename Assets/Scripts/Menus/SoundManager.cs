using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public float overallVolume = 0;

    private AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float v)
    {
        if (v < 0)
            return;
        if (v > 1)
            v = 1;

        overallVolume = v;
        audioSource.volume = overallVolume;
    }

    public void ChangeClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }
}

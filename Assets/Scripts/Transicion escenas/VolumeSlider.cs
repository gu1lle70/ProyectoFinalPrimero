using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private AudioReverbZone reverbZone;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            ChangeMusic();
            ChangeSFX();
        }
    }
    public void ChangeMusic()
    {
        float volume = musicSlider.value;  
        myMixer.SetFloat("music",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        CheckAndDisableReverb();
    }
    public void ChangeSFX()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        CheckAndDisableReverb();
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        ChangeMusic();
        ChangeSFX();
    }
    private void CheckAndDisableReverb()
    {
        const float threshold = 0.001f;

        if (musicSlider.value < threshold && sfxSlider.value < threshold)
        {
            reverbZone.enabled = false;
        }
        else
        {
            reverbZone.enabled = true;
        }
    }

}

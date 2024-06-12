using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public Toggle toggle;
    public ParticleSystem dust;
    public AudioMixerGroup sfxMixer;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        Screen.fullScreen = true;

        if (toggle != null)
        {
            toggle.isOn = !Screen.fullScreen;
        }
        else { toggle.isOn = false; }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void EnableObject(GameObject ob)
    {
        if (ob == null)
            return;

        ob.SetActive(true);
    }
    public void DisableObject(GameObject ob)
    {
        if (ob == null)
            return;

        ob.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MaximizeWindow(bool value)
    {
        if (toggle != null)
        {
            Screen.fullScreen = !toggle.isOn;
        };
    }
    public void CreateDust()
    {
            dust.Play();
    }
    public void GenerateSound(AudioClip clip)
    {
        GameObject ob = new GameObject("Throwable sound");
        AudioSource a = ob.AddComponent<AudioSource>();
        a.clip = clip;
        a.outputAudioMixerGroup = sfxMixer;
        a.Play();

        Destroy(ob, clip.length);
    }
}
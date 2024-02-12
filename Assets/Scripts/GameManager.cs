using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
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
        Screen.fullScreen = value;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject _menuPausa;
    [SerializeField] private GameObject _menuOptions;

    [SerializeField] private bool _inPause;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _inPause == false && PlayerMove.Instance.isNotInTutorial == true)
        {
            Pausa();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && _inPause == true)
        {
            Resume();
            _menuOptions.SetActive(false);
        }
    }
    public void Pausa()
    {
        Time.timeScale = 0f;
        _menuPausa.SetActive(true);
        _inPause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        _menuPausa.SetActive(false);
        _inPause = false;
    }

    public void Options()
    {
        _menuPausa.SetActive(false);
        _menuOptions.SetActive(true);
    }

    public void CloseOptipns()
    {
        _menuPausa.SetActive(true);
        _menuOptions.SetActive(false);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0f;
        _menuPausa.SetActive(false);
        _inPause = false;
    }
}

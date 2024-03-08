using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject _menuPausa;

    [SerializeField] private GameObject _botonPausa;
    public void Pausa()
    {
        Time.timeScale = 0f;
        _botonPausa.SetActive(false);
        _menuPausa.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        _botonPausa.SetActive(true);
        _menuPausa.SetActive(false);
    }
}

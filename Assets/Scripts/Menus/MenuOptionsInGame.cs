using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptionsInGame : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlayMenu;

    [SerializeField] private GameObject _optionsMenu;


    public void OpenHTP()
    {
        _optionsMenu.SetActive(false);
        _howToPlayMenu.SetActive(true);
    }

    public void CloseHTP()
    {
        _howToPlayMenu.SetActive(false); 
        _optionsMenu.SetActive(true);
    }
}

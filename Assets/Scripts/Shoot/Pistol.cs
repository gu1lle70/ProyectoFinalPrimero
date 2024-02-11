using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(bullet, transform);
        }
    }
}
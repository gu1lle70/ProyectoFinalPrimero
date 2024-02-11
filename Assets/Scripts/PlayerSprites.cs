using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSprites : MonoBehaviour
{
    public static PlayerSprites Instance { get; private set; }

    [HideInInspector] public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpriteDirection(InputAction.CallbackContext con)
    {
        if (con.ReadValue<Vector2>().x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (con.ReadValue<Vector2>().x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject button;

    private void Start()
    {
        transform.localScale = Vector3.one;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(button, Vector3.one * 2, 1);
        Debug.Log("puta madre");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(button, Vector3.one*4, 3);
        Debug.Log("no puta madre");
    }
}

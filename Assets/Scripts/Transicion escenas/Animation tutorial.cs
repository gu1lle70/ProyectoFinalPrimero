using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtutorial : MonoBehaviour
{
    [SerializeField] GameObject bg_top;
    [SerializeField] GameObject bg_down;

    private void Start()
    {
        StartAnimation();
    }


    private void StartAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -180, 1f).setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(EndAnimation);
    }

    private void EndAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 265, 1.2f);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -265, 1.2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAnimation : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject quit;

    // Start is called before the first frame update
    void Start()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        LeanTween.moveX(play.GetComponent<RectTransform>(), -287.4f, velocity).setDelay(0.5f);
        LeanTween.moveX(options.GetComponent<RectTransform>(), -287.4f, velocity).setDelay(0.6f);
        LeanTween.moveX(credits.GetComponent<RectTransform>(), -287.4f, velocity).setDelay(0.7f);
        LeanTween.moveX(quit.GetComponent<RectTransform>(), -287.4f, velocity).setDelay(0.8f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInTransition : MonoBehaviour
{
    [SerializeField] private GameObject black_bg;


    private void Start()
    {
        SubirAlpha();
    }

    private void SubirAlpha()
    {
        LeanTween.alpha(black_bg.GetComponent<RectTransform>(), 0f, 1.5f).setDelay(1f);
    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Instance;
    [SerializeField] private GameObject black_bg;

    public bool isEnd = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void BajarAlpha()
    {
        Debug.Log("empieza");
        LeanTween.alpha(black_bg.GetComponent<RectTransform>(), 255f, 1f);
        Debug.Log("acaba");
        isEnd = false;

    }

    private void Update()
    {
        if (isEnd == true)
        {
            BajarAlpha();
        }
    }
}
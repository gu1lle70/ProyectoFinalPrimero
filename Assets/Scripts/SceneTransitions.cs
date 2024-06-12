using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Instance;
    [SerializeField] private GameObject black_bg;
    [SerializeField] private string SceneName;


   

    private void BajarAlpha()
    {
        black_bg.SetActive(true);
        LeanTween.alpha(black_bg.GetComponent<RectTransform>(), 2f, 1f).setOnComplete(ChangeScene);
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BajarAlpha();
    }

    private void ChangeScene()
    {

        SceneManager.LoadScene(SceneName);
    }

}
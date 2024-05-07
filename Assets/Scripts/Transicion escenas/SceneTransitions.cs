using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Instance;
    [SerializeField] private GameObject black_bg;
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject NullObject;

   

    private void BajarAlpha()
    {
        LeanTween.alpha(black_bg.GetComponent<RectTransform>(), 2f, 1f).setOnComplete(ChangeScene);
        return;
    }

    public void Update()
    {
        if (NullObject == null)
        {
            BajarAlpha();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

}
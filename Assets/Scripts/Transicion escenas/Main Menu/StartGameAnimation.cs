using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private Image Block_Buttons;
    [SerializeField] private GameObject _BG_Izq;
    [SerializeField] private GameObject _BG_Der;
    [SerializeField] private GameObject Text1;
    [SerializeField] private GameObject Text2;
    [SerializeField] private GameObject Text3;
    [SerializeField] private AudioManager audioManager;
    public string sceneName;

    public void OptionsAnimationOpen()
    {
        LeanTween.moveY(_optionsMenu.GetComponent<RectTransform>(), 14.0f, 1.5f).setEase(LeanTweenType.easeOutBounce);
    }

    public void OptionsAnimationClose()
    {
        LeanTween.moveY(_optionsMenu.GetComponent<RectTransform>(), 958.0f, 1.5f).setEase(LeanTweenType.easeInBack);
    }


    public void StartAnimation()
    {
        Block_Buttons.gameObject.SetActive(true);
        Block_Buttons.raycastTarget = true;
        LeanTween.moveX(_BG_Izq.GetComponent<RectTransform>(), -488, 1.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveX(_BG_Der.GetComponent<RectTransform>(), 486, 1.5f).setEase(LeanTweenType.easeOutBounce).setOnComplete(StartText);
    }

    private void StartText()
    {
        Text1.SetActive(true);
        Text2.SetActive(true);
        Text3.SetActive(true);
        LeanTween.alpha(Text1.GetComponentInChildren<RectTransform>(), 0f, 2.0f).setDelay(1.0f);
        LeanTween.alpha(Text2.GetComponentInChildren<RectTransform>(), 0f, 2.0f).setDelay(6.0f);
        LeanTween.alpha(Text3.GetComponentInChildren<RectTransform>(), 0f, 2.0f).setDelay(14.0f);
        LeanTween.alpha(Block_Buttons.GetComponent<RectTransform>(),3f, 2.5f).setDelay(18.5f).setOnComplete(ChangeScene);
        
    }
    private void ChangeScene()
    {
        audioManager.FadeOutMusic();
        SceneManager.LoadScene(sceneName);
    }
}

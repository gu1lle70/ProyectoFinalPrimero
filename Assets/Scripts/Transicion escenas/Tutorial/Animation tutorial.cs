using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationTutorial : MonoBehaviour
{
    public static AnimationTutorial Instance { get; private set; }

    [SerializeField] GameObject bg_top;
    [SerializeField] GameObject bg_down;
    [SerializeField] GameObject character;
    [SerializeField] GameObject clickToContinue;
    [SerializeField] GameObject black_bg;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private float typingTime = 0.05f;
    [SerializeField] public bool hasFinished = false;

    private int lineIndex;

    private bool canPassDialogue;
    private bool animationEnded = false;
    private bool isFadingOut = true; // Nueva bandera para controlar el fade out

    private void Start()
    {
        FadeOut();
        hasFinished = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Si el fade out está en progreso o la animación no ha terminado, no hacer nada
            if (isFadingOut || !animationEnded) return;

            if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void FadeOut()
    {
        PlayerMove.Instance.isNotInTutorial = false;
        LeanTween.alpha(black_bg.GetComponent<RectTransform>(), 0f, 1.5f).setDelay(1f).setOnComplete(() =>
        {
            isFadingOut = false; // Actualizar bandera al completar fade out
            StartAnimation();
        });
    }

    private void StartAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveX(character.GetComponent<RectTransform>(), 330, 1f).setEase(LeanTweenType.easeInOutBack).setDelay(0.4f)
            .setOnComplete(() =>
            {
                animationEnded = true;
                StartDialogue();
            });
    }

    private void EndAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 280, 1.2f);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -280, 1.2f);
        LeanTween.moveX(character.GetComponent<RectTransform>(), 510, 1f).setEase(LeanTweenType.easeInOutBack);
        PlayerMove.Instance.isNotInTutorial = true;
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    private void StartDialogue()
    {
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    public void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndAnimation();
            
        }
    }

    private IEnumerator ShowLine()
    {
        clickToContinue.SetActive(false);
        dialogueText.text = string.Empty;
        yield return new WaitForSecondsRealtime(0.2f);
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
        clickToContinue.SetActive(true);
    }
}

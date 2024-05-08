using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animationtutorial : MonoBehaviour
{
    [SerializeField] GameObject bg_top;
    [SerializeField] GameObject bg_down;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private float typingTime = 0.05f;

    private int lineIndex;

    private bool canPassDialogue;
    private bool animationEnded = false;

    private void Start()
    {
        StartAnimation();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!animationEnded)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
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


    private void StartAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -180, 1f).setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(StartDialogue);
        animationEnded = true;
    }

    private void EndAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 280, 1.2f);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -280, 1.2f);
    }
    private void StartDialogue()
    {
        lineIndex = 0;
        Time.timeScale = 0f;
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
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        yield return new WaitForSecondsRealtime(0.5f);
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

    }
}

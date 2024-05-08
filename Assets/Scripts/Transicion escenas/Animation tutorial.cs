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

    private bool didDialogueStart;
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
            if (animationEnded)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex]) ;
            {
                NextDialogueLine();
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
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 265, 1.2f);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -265, 1.2f);
    }
    private void StartDialogue()
    {
        didDialogueStart = true;
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
            didDialogueStart = false;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        yield return new WaitForSeconds(1.2f);
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
}

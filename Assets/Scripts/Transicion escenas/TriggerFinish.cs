using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinish : MonoBehaviour
{

    [SerializeField] GameObject bg_top;
    [SerializeField] GameObject bg_down;
    [SerializeField] GameObject character;
    [SerializeField] GameObject clickToContinue;
    [SerializeField] GameObject dialogueMark;
    [SerializeField] GameObject black_bg;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private float typingTime = 0.05f;
    [SerializeField] private string SceneName;

    private int lineIndex;

    private bool didDialogeStart = false;
    public bool finishTutorial = false;

    private void Update()
    {
        if (finishTutorial && Input.GetButtonDown("Fire1"))
        {
            StartAnimation();
            if (!didDialogeStart)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueMark.SetActive(true);
            Debug.Log("empieza final tutorial");
            finishTutorial = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            dialogueMark.SetActive(false);
    }


    private void StartAnimation()
    {
        PlayerMove.Instance.isNotInTutorial = false;
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveX(character.GetComponent<RectTransform>(), 330, 1f).setEase(LeanTweenType.easeInOutBack);
    }

    private void EndAnimation()
    {
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 280, 1.2f);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -280, 1.2f);
        LeanTween.moveX(character.GetComponent<RectTransform>(), 510, 1f).setEase(LeanTweenType.easeInOutBack);
        PlayerMove.Instance.isNotInTutorial = true;
        dialogueMark.SetActive(true);
        StopAllCoroutines();
        LeanTween.alpha(black_bg.GetComponent<RectTransform>(), 2f, 1f).setOnComplete(ChangeScene);
    }
    private void StartDialogue()
    {
        dialogueMark.SetActive(false);
        didDialogeStart = true;
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

            didDialogeStart = false;
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

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

}

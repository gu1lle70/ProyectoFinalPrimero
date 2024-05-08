using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private float typingTime = 0.05f;

    private bool didDialogueStart;
    private int lineIndex;

    public bool canPassDialogue;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (dialogueText.text == dialogueLines[lineIndex])
        {
            canPassDialogue = true;
        }
    }

    public void StartDialogue()
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
            didDialogueStart=false;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationLvL3 : MonoBehaviour
{
    [Header("Animation dialogue")]
    [SerializeField] GameObject bg_top;
    [SerializeField] GameObject bg_down;
    [SerializeField] GameObject character;
    [SerializeField] GameObject clickToContinue;
    [SerializeField] BoxCollider2D box2D;

    [Space]

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float typingTime = 0.05f;

    [Header("Camera Shake")]
    [SerializeField] private float _duration;
    [SerializeField] private GameObject _camera;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private List<GameObject> _plataforms;
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject _collider2;
    [SerializeField] private GameObject _player;


    private int lineIndex;

    public bool startAnimation = false;
    private bool animationEnded = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (!animationEnded) return;
            
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

    IEnumerator Shaking()
    {
        
        Vector3 startPosition = _camera.transform.position;
        float time = 0f;
        while (time < _duration)
        {
            time += Time.deltaTime;
            float fuerza = _curve.Evaluate(time / _duration);
            _camera.transform.position = startPosition + Random.insideUnitSphere * fuerza;
            yield return null;
        }
        foreach (GameObject go in _plataforms)
        {
            go.AddComponent<Rigidbody2D>();
            go.GetComponent<Rigidbody2D>().freezeRotation = true;
            yield return new WaitForSeconds(0.07f);
        }
        CameraController.Instance.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collider.SetActive(false);
        PlayerMove.Instance._speed = 0;
        DASH.instance.enabled = false;
        DASH.instance.canDash = false;
        _player.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        _collider2.SetActive(true);        
        if (collision.tag == "Player")
        {
            Debug.Log("empieza dialogo con mago");
            StartAnimation();
            startAnimation = true;
        }
    }

    private void StartAnimation()
    {
        PlayerMove.Instance.isNotInTutorial = false;
        LeanTween.moveY(bg_top.GetComponent<RectTransform>(), 180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveY(bg_down.GetComponent<RectTransform>(), -180, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveX(character.GetComponent<RectTransform>(), 330, 1f).setEase(LeanTweenType.easeInOutBack)
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
        box2D.enabled = false;
        StartCoroutine(Shaking());
        
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

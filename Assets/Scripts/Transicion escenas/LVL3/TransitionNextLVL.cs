using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionNextLVL : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bgAnimation;
    public string sceneName;

    IEnumerator StartAnimation()
    {
        foreach (GameObject go in _bgAnimation)
        {
            LeanTween.moveX(go.GetComponent<RectTransform>(),805,0.5f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(sceneName);
        yield return null;

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(StartAnimation());

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private GameObject _camera;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private List<GameObject> _plataforms;
    [SerializeField] private GameObject _collider;

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
        _camera.transform.position = startPosition;
        foreach (GameObject go in _plataforms)
        {
            go.AddComponent<Rigidbody2D>();
            yield return new WaitForSeconds(0.07f);
        }
        _collider.SetActive(false);
        PlayerMove.Instance.isNotInTutorial = true;
        PlayerMove.Instance._speed = 7;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove.Instance.isNotInTutorial = false;
        PlayerMove.Instance._speed = 0;

        StartCoroutine(Shaking());
        
    }
}

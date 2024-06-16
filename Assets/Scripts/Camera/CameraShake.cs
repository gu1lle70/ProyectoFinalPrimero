using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private GameObject _camera;
    [SerializeField] private AnimationCurve _curve;

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
        PlayerMove.Instance.isNotInTutorial = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove.Instance.isNotInTutorial = false;
        StartCoroutine(Shaking());
        
    }
}

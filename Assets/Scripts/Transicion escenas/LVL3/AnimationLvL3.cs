using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLvL3 : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private GameObject _camera;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private List<GameObject> _plataforms;
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject _collider2;
    [SerializeField] private GameObject _player;

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
        StartCoroutine(Shaking());
        
    }
}

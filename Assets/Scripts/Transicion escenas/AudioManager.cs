using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;

    public float fadeDuration = 1.0f;

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOut(menuMusic, fadeDuration));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restablecer el volumen para la próxima vez que se reproduzca
    }
}

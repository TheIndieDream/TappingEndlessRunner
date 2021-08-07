using System.Collections;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private FloatVariable transitionTime;
    [SerializeField] private AudioClip titleScreenMusic;
    [SerializeField] private AudioClip gameScreenMusic;

    public void ToGame()
    {
        StopAllCoroutines();
        StartCoroutine(Transition(gameScreenMusic));
    }

    public void ToTitle()
    {
        StopAllCoroutines();
        StartCoroutine(Transition(titleScreenMusic));
    }

    private IEnumerator Transition(AudioClip transitionToClip)
    {
        float elapsedTime = 0.0f;
        float startVolume = musicAudio.volume;
        float halfTime = transitionTime.Value * 0.5f;
        while (elapsedTime < halfTime)
        {
            musicAudio.volume = Mathf.Lerp(startVolume, 0.0f, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        musicAudio.volume = 0.0f;

        musicAudio.clip = transitionToClip;
        musicAudio.Play();

        elapsedTime = 0.0f;
        while (elapsedTime < halfTime)
        {
            musicAudio.volume = Mathf.Lerp(0.0f, 1.0f, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        musicAudio.volume = 1.0f;
    }
}

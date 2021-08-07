using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transitioner : MonoBehaviour
{
    [SerializeField] private Image transitionFader;
    [SerializeField] private GameEvent toGame;
    [SerializeField] private GameEvent toTitle;
    [SerializeField] private FloatVariable transitionTime;

    public void ToGame()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(toGame));
    }

    public void ToTitle()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(toTitle));
    }

    private IEnumerator TransitionRoutine(GameEvent transitionEvent)
    {
        float elapsedTime = 0.0f;
        float halfTime = transitionTime.Value * 0.5f;
        while(elapsedTime < halfTime)
        {
            transitionFader.color = 
                Color.Lerp(Color.clear, Color.white, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transitionFader.color = Color.white;

        transitionEvent.Raise();

        elapsedTime = 0.0f;
        while(elapsedTime < halfTime)
        {
            transitionFader.color =
                Color.Lerp(Color.white, Color.clear, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transitionFader.color = Color.clear;
    }
}

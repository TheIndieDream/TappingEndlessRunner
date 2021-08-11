using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transitioner : MonoBehaviour
{
    [SerializeField] private Image transitionFader;
    [SerializeField] private CanvasGroup transitionGroup;
    [SerializeField] private GameLoopHandler gameLoopHandler;
    [SerializeField] private GameEvent toGame;
    [SerializeField] private GameEvent gameReset;
    [SerializeField] private FloatVariable transitionTime;

    private IEnumerator Start()
    {
        yield return FadeInRoutine();
    }

    public void ResetGame()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(gameReset));
    }

    public void ToGame()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(toGame));
    }

    public void OnGameRestart()
    {
        StartCoroutine(RestartGameRoutine());
    }

    private IEnumerator RestartGameRoutine()
    {
        yield return FadeOutRoutine();
        gameLoopHandler.Restart();
    }

    private IEnumerator FadeOutRoutine()
    {
        transitionGroup.blocksRaycasts = true;
        float elapsedTime = 0.0f;
        float halfTime = transitionTime.Value * 0.5f;
        while (elapsedTime < halfTime)
        {
            transitionFader.color =
                Color.Lerp(Color.clear, Color.white, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transitionFader.color = Color.white;
    }

    private IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0.0f;
        float halfTime = transitionTime.Value * 0.5f;
        while (elapsedTime < halfTime)
        {
            transitionFader.color =
                Color.Lerp(Color.white, Color.clear, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transitionFader.color = Color.clear;
        transitionGroup.blocksRaycasts = false;
    }

    private IEnumerator TransitionRoutine(GameEvent transitionEvent)
    {
        yield return FadeOutRoutine();
        transitionEvent.Raise();
        yield return FadeInRoutine();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameLoopHandler : ScriptableObject
{
    [SerializeField] private GameEvent gamePaused;
    private bool paused = false;

    private void OnEnable()
    {
        paused = false;
    }

    public void Pause()
    {
        gamePaused.Raise();
        if (paused)
        {
            paused = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            paused = true;
            Time.timeScale = 0.0f;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

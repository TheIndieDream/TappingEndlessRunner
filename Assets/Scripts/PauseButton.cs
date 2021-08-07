using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private bool gamePaused = false;
    public void Pause()
    {
        if (gamePaused)
        {
            gamePaused = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            gamePaused = true;
            Time.timeScale = 1.0f;
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

using UnityEngine;

/// <summary>
/// Responsible for updating the score and bonus count based on gameplay.
/// </summary>
public class ScoreTracker : MonoBehaviour
{
    [Tooltip("Score variable to update.")]
    [SerializeField] private IntVariable score;

    [Tooltip("Bonus variable to update")]
    [SerializeField] private IntVariable bonus;

    [Tooltip("Current game speed.")]
    [SerializeField] private FloatVariable gameSpeed;

    /// <summary>
    /// Stores the current score as a float for conversion to integer for 
    /// display.
    /// </summary>
    private float scoreFloat = 0;

    private bool scoring = false;

    private void Awake()
    {
        score.Value = 0;
        bonus.Value = 0;
    }

    private void Update()
    {
        UpdateScore();
    }

    public void OnGameReset()
    {
        scoring = false;
    }

    public void OnGameStart()
    {
        scoring = true;
    }

    /// <summary>
    /// Increases the score based on game speed then converts the results to
    /// an integer for display.
    /// </summary>
    private void UpdateScore()
    {
        if (scoring)
        {
            scoreFloat += gameSpeed.Value * Time.deltaTime;
            score.Value = Mathf.RoundToInt(scoreFloat);
        }
    }
}

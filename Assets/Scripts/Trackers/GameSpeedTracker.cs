using UnityEngine;

/// <summary>
/// Responsible for increasing the game speed ambiently as the game progresses.
/// </summary>
public class GameSpeedTracker : MonoBehaviour
{
    [Tooltip("Float representing the game speed.")]
    [SerializeField] private FloatVariable gameSpeed;

    [Tooltip("Starting speed for the game.")]
    [SerializeField] private float startingGameSpeed = 4.0f;

    [Tooltip("The game speed will increase by this much each interval.")]
    [SerializeField] private float speedDelta;

    [Tooltip("Game will speed up every ____ seconds.")]
    [SerializeField] private float speedUpInterval;

    private bool accelerating = false;

    private void Awake()
    {
        gameSpeed.Value = 0.0f;
    }

    private void Update()
    {
        if (accelerating)
        {
            IncreaseGameSpeed();
        }
    }

    public void OnTutorialEnd()
    {
        accelerating = true;
    }

    public void OnGameStart()
    {
        gameSpeed.Value = startingGameSpeed;
    }

    /// <summary>
    /// Increases the game speed by speedDelta every speedUpInterval seconds.
    /// </summary>
    private void IncreaseGameSpeed()
    {
        gameSpeed.Value += (speedDelta / speedUpInterval) * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour
{
    [SerializeField] GameEvent postHighScore;

    [Header("Text UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private TMP_InputField initialsInput;

    [Header("Score Values")]
    [SerializeField] private StringVariable playerName;
    [SerializeField] private IntVariable playerScore;
    [SerializeField] private IntVariable playerBonus;
    [SerializeField] private IntVariable playerFinalScore;
    [SerializeField] private IntVariable minHighScore;
    [SerializeField] private IntVariable bonusMultiplier;

    private bool postScore = false;

    private void Start()
    {
        playerName.Value = "";
    }

    public void UpdatePlayerName(string name)
    {
        playerName.Value = name;
    }

    public void OnPlayerDied()
    {
        UpdateScoreText();
    }

    public void OnGameReset()
    {
        if (postScore)
        {
            postHighScore.Raise();
            postScore = false;
        }
        playerName.Value = "";
    }

    public void OnGameRestart()
    {
        if (postScore)
        {
            postHighScore.Raise();
            postScore = false;
        }
        playerName.Value = "";
    }

    private void UpdateScoreText()
    {
        newHighScoreText.gameObject.SetActive(false);
        initialsInput.gameObject.SetActive(false);
        postScore = false;

        scoreText.text = "Your Score: " + playerScore.Value;
        bonusText.text = "Bonus (x10): +" + playerBonus.Value * bonusMultiplier.Value;
        finalScoreText.text = "Final Score: " + playerFinalScore.Value;

        if(playerFinalScore.Value > minHighScore.Value)
        {
            newHighScoreText.gameObject.SetActive(true);
            initialsInput.gameObject.SetActive(true);
            postScore = true;
        }
    }

    
}

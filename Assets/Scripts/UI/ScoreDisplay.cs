using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for displaying the entity's score and bonus count during 
/// gameplay.
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private ColorVariable bonusColor;

    [SerializeField] private IntVariable score;
    [SerializeField] private IntVariable bonus;

    private void Start()
    {
        OnBonusCollected();
        UpdateScoreText();
    }

    private void Update()
    {
        if(bonusText.color != bonusColor.Value)
        {
            bonusText.color = bonusColor.Value;
        }
    }

    private void LateUpdate()
    {
        UpdateScoreText();
    }

    public void OnBonusCollected()
    {
        bonusText.text = "Bonus: " + bonus.Value;
    }

    public void OnGameStart()
    {
        OnBonusCollected();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.Value;
        bonusText.text = "Bonus: " + bonus.Value;
    }
}

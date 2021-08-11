using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class HighscoreHandler : MonoBehaviour
{
    private const string highscoreURL = "https://brandonlymangamedev.com/neonlineshighscore.php";

    [SerializeField] private GameEvent highScoresLoaded;
    [SerializeField] private StringVariable currentPlayerName;
    [SerializeField] private IntVariable currentPlayerScore;
    [SerializeField] private IntVariable minHighScore;
    [SerializeField] private TextMeshProUGUI[] namesTextUI;
    [SerializeField] private TextMeshProUGUI[] scoresTextUI;

    private List<Score> scores;

    private IEnumerator Start()
    {
        yield return RetrieveScoresRoutine();
        int minScoreIndex = Mathf.Min(scores.Count - 1, 4);
        minHighScore.Value = scores[minScoreIndex].score;
    }

    public void LoadHighScores()
    {
        StartCoroutine(LoadHighScoresRoutine());
    }

    public void PostHighScore()
    {
        StartCoroutine(PostScoreRoutine(currentPlayerName.Value, 
            currentPlayerScore.Value));
    }

    private IEnumerator LoadHighScoresRoutine()
    {
        yield return RetrieveScoresRoutine();
        int scoresToParse = Mathf.Min(namesTextUI.Length, scores.Count);
        for (int i = 0; i < scoresToParse; i++)
        {
            namesTextUI[i].text = 
                "<color=#FF00F2>" + scores[i].name + "</color>";
            scoresTextUI[i].text = 
                "<color=#00FFFF>" + scores[i].score.ToString() + "</color>";
        }
        highScoresLoaded.Raise();
    }

    private IEnumerator PostScoreRoutine(string name, int score)
    {
        // Remove white space from name.
        string processedName = 
            String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));

        // Ensure name is uppercase.
        processedName = processedName.ToUpper();

        // Do not post name if it is empty.
        if (String.IsNullOrEmpty(processedName))
        {
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", processedName);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }

    private IEnumerator RetrieveScoresRoutine()
    {
        scores = new List<Score>();

        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            entry.score = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }

                        scores.Add(entry);
                    }
                }
            }
        }
    }

    
}

public struct Score
{
    public string name;
    public int score;
}
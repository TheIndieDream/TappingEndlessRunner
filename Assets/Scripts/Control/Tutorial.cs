using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialObjects;
    [SerializeField] private GameEvent gameStarted;

    public void EndTutorial()
    {
        foreach(GameObject tutorialObject in tutorialObjects)
        {
            tutorialObject.SetActive(false);
        }
        gameStarted.Raise();
    }
}

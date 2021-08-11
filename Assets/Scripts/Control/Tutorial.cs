using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialObjects;
    [SerializeField] private Vector3[] tutorialObjectStartPositions;
    [SerializeField] private GameEvent gameStarted;
    [SerializeField] private BoolVariable tutorialActive;

    private void Start()
    {
        tutorialActive.Value = true;

        tutorialObjectStartPositions = new Vector3[tutorialObjects.Length];

        for (int i = 0; i < tutorialObjectStartPositions.Length; i++)
        {
            tutorialObjectStartPositions[i] = 
                tutorialObjects[i].transform.position;
        }
    }

    public void EndTutorial()
    {
        tutorialActive.Value = false;
        DeactivateTutorialObjects();
        gameStarted.Raise();
    }

    public void DeactivateTutorialObjects()
    {
        foreach (GameObject tutorialObject in tutorialObjects)
        {
            tutorialObject.SetActive(false);
        }
    }

    public void OnGameReset()
    {
        tutorialActive.Value = true;
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialObjects[i].SetActive(true);
            tutorialObjects[i].transform.position = 
                tutorialObjectStartPositions[i];
        }
    }
}

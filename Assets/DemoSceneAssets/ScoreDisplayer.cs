using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] Story assignedStory;
    public TMP_Text scoreText;
    private int speakerConnection;
    
    // Start is called before the first frame update
    void Start()
    {
        DemoInkManager.OnCreateStory += ObserveStoryVar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //with ObserverVariable method requires to subscribe to OnCreateStory
    void ObserveStoryVar(Story currentStory)
    {
        if (currentStory != null)
        {
            if (currentStory.variablesState["kimConnection"] != null)
            {
                currentStory.ObserveVariable("kimConnection", (string varName, object newValue)
                =>
                { SetConnectionScore((int)newValue); });
            }

        }

    }
    void SetConnectionScore(int newScore) //redundant with button listener
    {
        Debug.Log("Score should be set to " + newScore);
        if (scoreText != null)
        {
            scoreText.text = newScore.ToString();
        }
    }
}


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
    
    void ObserveStoryVar(Story currentStory)
    {
        if (currentStory != null)
        {
            if (currentStory.variablesState["kimConnection"] != null)
            {
                //speakerConnection = (int)currentStory.variablesState["kimConnection"];
                //Debug.Log(speakerConnection);


                currentStory.ObserveVariable("kimConnection", (string varName, object newValue)
                => { SetConnectionScore((int)newValue); });
            }
            
        }

        //Debug.Log("Observed");
        
    }
    void SetConnectionScore(int newScore)
    {
        Debug.Log("Score should be set to "+ newScore);
        if (scoreText != null)
        {
            scoreText.text = newScore.ToString();
        }
    }
}


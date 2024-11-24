using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ManagerStrings : MonoBehaviour
{
    [SerializeField]
    private GameObject stringPrefab;
    [SerializeField]
    List<RelationshipString> createdStrings = new List<RelationshipString>();

    int connectionScore;

    // Start is called before the first frame update
    void Start()
    {
        FindFirstObjectByType<Player>().MakeString += MakeString;
        FindFirstObjectByType<ManagerInk>().OnCreateStory += ObserveDialogue;
        connectionScore = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Given 2 transforms, create a RelationshipString and add it to the list.
    void MakeString(Transform from, Transform to)
    {
        GameObject newString = Instantiate(stringPrefab);
        RelationshipString rString = newString.GetComponent<RelationshipString>();
        
        createdStrings.Add(rString);
        rString.CreateFullString(from, to, createdStrings.Count - 1, 3, connectionScore);
    }

    //Example for reading & keeping track of variables within the INK story.
    void ObserveDialogue(Story currDialogue)
    {
        if (currDialogue == null)
            return;
        //Brute Force observing all variables individually since it's small and easy for now.
        
        //Kim Connection
        if (currDialogue.variablesState["kimConnection"] != null)
        {
            currDialogue.ObserveVariable("kimConnection", (string varName, object newValue)
            => { SetConnectionScore((int)newValue); });
        }

        //Make String should be called somewhere here, after the dialogue is *over*.
    }

    //Function that updates "Connection Score."
    void SetConnectionScore(int val)
    {
        Debug.Log(val);
        connectionScore = val;
    }

    //Goes through the CreatedString list and updates each String's Anxiety Reduction.
    public float GetAnxiety()
    {
        float retAnxiety = 0;
        foreach(RelationshipString rString in createdStrings) 
        {
            retAnxiety += rString.AnxietyTick();
        }
        return retAnxiety;
    }
}

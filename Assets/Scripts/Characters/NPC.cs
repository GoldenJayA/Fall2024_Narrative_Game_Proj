using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System;
using UnityEngine;

public class NPC : Character
{
    [SerializeField]
    private TextAsset inkJSONAsset = null;
    [SerializeField]
    private Story story;


    public event Action<Story> OnSelectStory;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void InitializeTopics(TopicList topics)
    {
        topicList = topics;
    }

    /*
     * void GenerateTopics(Story myScript)
     * {
     *  //Read the emotions & topic lists from the Story.
     * 
     * }
     */

    void ScheduleUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TalkToNPC(gameObject, story);
            
        }
    }
}

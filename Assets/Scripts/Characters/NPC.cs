using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Character
{
    [SerializeField]
    protected TextAsset inkJSONAsset = null;
    [SerializeField]
    protected Story story; //Respective story for each NPC.
    [SerializeField]
    protected List<ScheduleEvent> mySchedule; //Schedule of events for the NPC to pathfind towards.
    [SerializeField]
    protected NavMeshAgent pathfinding;


    private int eventIndex;
    public event Action<Story> OnSelectStory;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);
        FindFirstObjectByType<ManagerTime>().TimeUpdate += ScheduleUpdate;
        eventIndex = -1;
        pathfinding.stoppingDistance = 2;
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

    //"Interact" function, which determines if the player is talking to this NPC.
    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TalkToNPC(gameObject, story);
            
        }
    }

    //Based on the current time, determine if the NPC needs to move to the next destination.
    //*NOTE:* the events MUST be listed in chronological order, or everything fails.
    protected void ScheduleUpdate(float hours, float minutes)
    {
        if (eventIndex >= mySchedule.Count - 1)
            return;

        ScheduleEvent nextEvent = mySchedule[eventIndex + 1];
        if(nextEvent.hourTime <= hours && nextEvent.minuteTime <= minutes)
        {
            pathfinding.SetDestination(nextEvent.location.position);
            eventIndex++;
        }
    }

    //Extra function for making events outside of the inspector, if needed.
    protected void CreateScheduleEvent(Transform location, float hour, float minute)
    {
        ScheduleEvent newEvent;
        newEvent.location = location;
        newEvent.hourTime = hour;
        newEvent.minuteTime = minute;
        mySchedule.Add(newEvent);
    }
}

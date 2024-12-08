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
    [SerializeField]
    protected GameObject relStringPoint;

    [SerializeField]
    protected Animator animator;

    private bool isWalking, isTalking;
    private int eventIndex;
    public event Action<TextAsset> OnSelectStory;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);
        FindFirstObjectByType<ManagerTime>().TimeUpdate += ScheduleUpdate;
        FindFirstObjectByType<ManagerInk>().FinishedTalking += FinishTalking;
        eventIndex = -1;
        pathfinding.stoppingDistance = 2;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = pathfinding.velocity.magnitude > 0f;
        UpdateAnim();
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
            transform.LookAt(other.transform.position);
            isTalking = true;
            pathfinding.isStopped = true;
            pathfinding.velocity = Vector3.zero;
            other.GetComponent<FirstPersonPlayer>().TalkToNPC(relStringPoint, inkJSONAsset);
        }
    }

    //Based on the current time, determine if the NPC needs to move to the next destination.
    //*NOTE:* the events MUST be listed in chronological order, or everything fails.
    protected void ScheduleUpdate(float hours, float minutes)
    {
        if (eventIndex >= mySchedule.Count - 1)
        {
            animator.SetBool("isWalking", false);
            return;
        }
            

        ScheduleEvent nextEvent = mySchedule[eventIndex + 1];
        if(nextEvent.hourTime <= hours && nextEvent.minuteTime <= minutes)
        {
            pathfinding.SetDestination(nextEvent.location.position);
            animator.SetBool("isWalking", true);
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

    protected void UpdateAnim()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isTalking", isTalking);
    }

    protected void FinishTalking()
    {
        isTalking = false;
        pathfinding.isStopped = false;
    }
}

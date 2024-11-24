using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            other.GetComponent<Player>().TalkToNPC(transform);
            
        }
    }
}

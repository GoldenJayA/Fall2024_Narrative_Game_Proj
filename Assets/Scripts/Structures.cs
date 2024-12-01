using System;
using UnityEngine;


//Basic Data Structures for this game.
public struct Topic
{
    //Topics have an Interest Level and a Knowledge Level (see the Enums).
    //It determines how interested & knolwedgeable a character is about a topic.

    InterestLevel interest;
    KnowledgeLevel knowledge;
}


public struct TopicList
{
    //Topics are compiled into a single TopicList that each character has.
    //To ensure consistency across all characters, there is only this TopicList structure. Topics are based on the names within.

    Topic videoGames;
    Topic sports;
    Topic academics;
    Topic moviesTV;
    Topic celebrities;
}

[Serializable]
public struct ScheduleEvent
{
    //ScheduleEvents have a Transform (mainly position), and a time for going to that position, recorded in hours and minutes.
    //This is used by NPCs for purposes of making them move around the Party at set time points.

    public Transform location;
    public float hourTime;
    public float minuteTime;
}


public enum InterestLevel
{
    //Ennumerated Interst Level, scaling from 1-5. Interest level is tied to how willing a character is to learn about a topic.

    AGAINST, NEUTRAL, MILD, INTERESTED, DESIRED
}

public enum KnowledgeLevel
{
    //Ennuymerated Knowledge Level, scaling from 1-5. Knowledge level is tied to how much a character knows about a topic.

    AGAINST, LITTLE, AVERAGE, PROFICIENT, EXPERTISE
}

/* READING AND WRITING VARIABLES TO & FROM INK
 * StoryObj.varableState["VariableName"] -- Returns the value.
 * StoryObj.ObserverVariable("VariableName", (string varName, object newValue) => { FunctionName((args)newValue);}); 
 *      -- Calls FunctionName when VariableName is updated in Ink.
 */

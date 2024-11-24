public struct Topic
{
    InterestLevel interest;
    KnowledgeLevel knowledge;
}

public struct TopicList
{
    Topic videoGames;
    Topic sports;
    Topic academics;
    Topic moviesTV;
    Topic celebrities;
}


public enum InterestLevel
{
    AGAINST, NEUTRAL, MILD, INTERESTED, DESIRED
}

public enum KnowledgeLevel
{
    AGAINST, LITTLE, AVERAGE, PROFICIENT, EXPERTISE
}

/* READING AND WRITING VARIABLES TO & FROM INK
 * StoryObj.varableState["VariableName"] -- Returns the value.
 * StoryObj.ObserverVariable("VariableName", (string varName, object newValue) => { FunctionName((args)newValue);}); 
 *      -- Calls FunctionName when VariableName is updated in Ink.
 */
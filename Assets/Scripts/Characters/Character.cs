using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //All characters have a topic list and a name.
    [SerializeField]
    protected TopicList topicList;
    [SerializeField]
    protected string characterName;
}

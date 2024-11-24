using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNPCDialogueHolder : MonoBehaviour
{
    public static event Action<TextAsset> OnSelectStory;

    [SerializeField]
    private TextAsset inkJSONAsset;
    public Story story;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);
        DemoPlayerController.OnInteractNPC += SelectNPC;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SelectNPC(GameObject npc)
    {
        //Story selectedStory = npc.GetComponent<DemoNPCDialogueHolder>().story;
        TextAsset selectedStoryJSON = npc.GetComponent<DemoNPCDialogueHolder>().inkJSONAsset;
        OnSelectStory?.Invoke(selectedStoryJSON);
    }
}

using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNPCDialogueHolder : MonoBehaviour
{
    public static event Action<Story> OnSelectStory;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
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
        Story selectedStory = npc.GetComponent<DemoNPCDialogueHolder>().story;
        OnSelectStory?.Invoke(selectedStory);
    }
}

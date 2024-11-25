using UnityEngine;
using UnityEditor;
using Ink.Runtime;
using System;
using UnityEngine.UI;
using TMPro;

public class DemoInkManager : MonoBehaviour
{

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story currentStory;

    [SerializeField]
    private GameObject dialogueUIParent;
    [SerializeField]
    private GameObject convoPanelPrefab;
    private GameObject convoPanelObj;

    // UI Prefabs
    [SerializeField]
    private TMP_Text spkrNamePrefab = null;
    [SerializeField]
    private TMP_Text spkrTextPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;

    //story state
    public DialogueVariables dialogueVariables;
    private bool closeDialguePanel;
    

    public static event Action<Story> OnCreateStory;

    // Start is called before the first frame update
    private void Awake()
    {
        RemoveChildren(); //clears all UI story elements
        //StartStory();
        dialogueVariables = new DialogueVariables();
    }

    private void Start()
    {
        DemoNPCDialogueHolder.OnSelectStory += StartStory;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentStory.currentChoices.Count);
    }

    void StartStory(TextAsset npcStoryJSON)
    {
        //assigns Story varible to text of Ink JSON asset
        currentStory = new Story(npcStoryJSON.text); //declaring a new Story allows coversations to be started every interaction
        dialogueVariables.StartListening(currentStory); //dialogue variables listens for variable changes in current story
        if (OnCreateStory != null) OnCreateStory(currentStory); //passes current story to any listeners
        closeDialguePanel = false;
        currentStory.BindExternalFunction("quitDialogue", () => { CloseDialoguePanel(); });//for some reason called twice
        RefreshView(); //creates and refreshes UI panel
    }
    
    void RefreshView()
    {
        RemoveChildren();
        while (currentStory.canContinue)
        {
            string text = currentStory.Continue(); //Continue gets the next line of the story
            text = text.Trim(); //removes extra white space
            CreateContentView(text); //display on screen

            if (currentStory.currentChoices.Count > 0)
            {
                //moving through loop backwards so choices appear in the order they were written in Inkey
                for (int i = currentStory.currentChoices.Count - 1; i >= 0; i--)
                {
                    Choice choice = currentStory.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    button.onClick.AddListener(delegate
                    {
                        OnClickChoiceButton(choice);
                    });
                }
            }
            if (closeDialguePanel)
            {
                if (convoPanelObj != null)
                {
                    RemoveChildren();
                }
            }
        }
    }
    
    void OnClickChoiceButton(Choice choice)
    {
        currentStory.ChooseChoiceIndex(choice.index);
        RemoveChildren();
        RefreshView();
    }
    
    void CreateContentView(string text) //creates NPC dialogue view
    {
        GameObject convoPanel = Instantiate(convoPanelPrefab, dialogueUIParent.transform); //creates background panel
        convoPanelObj = convoPanel; //assigns reference to script-wide variable
        TMP_Text speakerName = Instantiate(spkrNamePrefab) as TMP_Text; //instantiates object for Speaker's Name
        speakerName.text = (string)currentStory.variablesState["NPCName"];
        speakerName.transform.SetParent(convoPanel.transform, false);
        TMP_Text speakerText = Instantiate(spkrTextPrefab) as TMP_Text; //instantiates object for what speaker says
        speakerText.text = text;
        speakerText.transform.SetParent(convoPanel.transform, false);
        speakerText.transform.SetSiblingIndex(1); //sets text to appear second on panel
    }
    
    Button CreateChoiceView(string text) //creates PC choice buttons
    {
        Button choice = Instantiate(buttonPrefab);

        if (convoPanelObj != null)
        {
            choice.transform.SetParent(convoPanelObj.transform, false);
            choice.transform.SetSiblingIndex(2);
            TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
            choiceText.text = text;
            VerticalLayoutGroup choiceLayoutGroup = choice.GetComponentInParent<VerticalLayoutGroup>();
        }
        return choice;
    }

    void RemoveChildren() //called after dialogue option selected
    {
        int childCount = dialogueUIParent.transform.childCount;
        for (int i = childCount -1; i>= 0; i--)
        {
            Destroy(dialogueUIParent.transform.GetChild(i).gameObject);
        }
    }

    void CloseDialoguePanel()
    {
        closeDialguePanel = true;
        dialogueVariables.StopListening(currentStory); //stop listening to current story variable changes
    }

}

using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.UI;
using TMPro;

public class ManagerInk : MonoBehaviour
{
    //INK Plugin Assets
    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story currentStory;

    //UI Elements where dialogue tree appears.
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
    [SerializeField]
    private DialogueVariables dialogueVars;
    

    public event Action<Story> OnCreateStory;
    public event Action FinishedTalking, StartTalking;
    private bool closeDialoguePanel;
    // Start is called before the first frame update
    private void Awake()
    {
        RemoveChildren(); //clears all UI story elements
        //StartStory();
        dialogueVars = new DialogueVariables();
    }

    private void Start()
    {
        FindFirstObjectByType<FirstPersonPlayer>().SelectDialogue += StartStory;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(story.variablesState["kimConnection"]);
    }

    private void StartStory(TextAsset npcStory)
    {
        StartTalking?.Invoke();
        //assigns Story varible to text of Ink JSON asset
        currentStory = new Story(npcStory.text);
        dialogueVars.StartListening(currentStory);
        if (OnCreateStory != null) 
            OnCreateStory(currentStory);
        closeDialoguePanel = false;
        currentStory.BindExternalFunction("quitDialogue", () => { CloseDialoguePanel(); });
        RefreshView();
    }
    
    void RefreshView()
    {
        //Debug.Log("Refresh view called: dialogue panel should appear");
        RemoveChildren();
        while (currentStory.canContinue)
        {
            string text = currentStory.Continue(); //Continue gets the next line of the story
            text = text.Trim(); //removes extra white space
            CreateContentView(text); //display on screen


            //Creates buttons based on number of options in the INK story.
            if (currentStory.currentChoices.Count > 0)
            {
                for (int i = currentStory.currentChoices.Count-1; i >= 0; i--)
                {
                    Choice choice = currentStory.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    button.onClick.AddListener(delegate
                    {
                        OnClickChoiceButton(choice);
                    });
                }
            }
            if(closeDialoguePanel)
            {
                if(convoPanelObj != null)
                {
                    RemoveChildren();
                }
            }
        }
    }
    
    //Handler for dialogue tree buttons; Wipes current UI & generates new options.
    void OnClickChoiceButton(Choice choice)
    {
        currentStory.ChooseChoiceIndex(choice.index);
        RemoveChildren();
        RefreshView();
    }
    
    //Creates the dialogue box with the passed text.
    void CreateContentView(string text)
    {
        //Debug.Log(text);
        GameObject convoPanel = Instantiate(convoPanelPrefab, dialogueUIParent.transform);
        convoPanelObj = convoPanel;
        TMP_Text speakerName = Instantiate(spkrNamePrefab) as TMP_Text;
        speakerName.transform.SetParent(convoPanel.transform, false);
        speakerName.text = (string)currentStory.variablesState["NPCName"];

        TMP_Text speakerText = Instantiate(spkrTextPrefab) as TMP_Text;
        //Debug.Log("text should instantiate");
        speakerText.text = text;
        speakerText.transform.SetParent(convoPanel.transform, false);
        speakerText.transform.SetSiblingIndex(1);
    }
    
    //Creates the buttons for the given dialogue options.
    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
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

    //Wipes the dialogue tree panel of all assets.
    void RemoveChildren()
    {
        int childCount = dialogueUIParent.transform.childCount;
        for (int i = childCount -1; i>= 0; i--)
        {
            Destroy(dialogueUIParent.transform.GetChild(i).gameObject);
        }
    }

    void CloseDialoguePanel()
    {
        closeDialoguePanel = true;
        dialogueVars.StopListening(currentStory); //stop listening to current story variable changes
        FinishedTalking?.Invoke();
        Debug.Log("invoked finished talking");
    }
}

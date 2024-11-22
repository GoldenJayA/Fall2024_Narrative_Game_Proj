using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.UI;
using TMPro;

public class ManagerInk : MonoBehaviour
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

    public event Action<Story> OnCreateStory;

    // Start is called before the first frame update
    private void Awake()
    {
        RemoveChildren(); //clears all UI story elements
        //StartStory();
    }

    private void Start()
    {
        FindFirstObjectByType<Player>().SelectDialogue += StartStory;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(story.variablesState["kimConnection"]);
    }

    void StartStory(Story npcStory)
    {
        //assigns Story varible to text of Ink JSON asset
        currentStory = npcStory;
        if (OnCreateStory != null) OnCreateStory(currentStory);
        RefreshView();
    }
    
    void RefreshView()
    {
        //Debug.Log("Refresh view called: dialogue panel should appear");
        //RemoveChildren();
        
        while (currentStory.canContinue)
        {
            string text = currentStory.Continue(); //Continue gets the next line of the story
            text = text.Trim(); //removes extra white space
            CreateContentView(text); //display on screen
        }

        if (currentStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < currentStory.currentChoices.Count; i++)
            {
                Choice choice = currentStory.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
        {
            Button choice = CreateChoiceView("End \n Restart?");
            choice.onClick.AddListener(delegate { TallyScore(); });
            choice.onClick.AddListener(delegate {
                StartStory(currentStory);
            });
        }
    }
    
    void OnClickChoiceButton(Choice choice)
    {
        currentStory.ChooseChoiceIndex(choice.index);
        RemoveChildren();
        RefreshView();
    }
    
    void CreateContentView(string text)
    {
        Debug.Log(text);
        GameObject convoPanel = Instantiate(convoPanelPrefab, dialogueUIParent.transform);
        convoPanelObj = convoPanel;
        TMP_Text speakerName = Instantiate(spkrNamePrefab) as TMP_Text;
        TMP_Text speakerText = Instantiate(spkrTextPrefab) as TMP_Text;
        Debug.Log("text should instantiate");
        speakerText.text = text;
        speakerText.transform.SetParent(convoPanel.transform, false);
        speakerText.transform.SetSiblingIndex(1);
    }
    
    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        if (convoPanelObj != null)
        {
            choice.transform.SetParent(convoPanelObj.transform, false);
            choice.transform.SetSiblingIndex(2);
        }

        TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
        choiceText.text = text;

        VerticalLayoutGroup choiceLayoutGroup = choice.GetComponentInParent<VerticalLayoutGroup>();
        //choiceLayoutGroup.childForceExpandWidth = true;
        //choiceLayoutGroup.childForceExpandHeight = false;

        return choice;
    }

    void RemoveChildren()
    {
        int childCount = dialogueUIParent.transform.childCount;
        for (int i = childCount -1; i>= 0; i--)
        {
            Destroy(dialogueUIParent.transform.GetChild(i).gameObject);
        }
    }

    void TallyScore()
    {

    }
}

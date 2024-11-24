using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUISpawner : MonoBehaviour
{
    public RectTransform UIPanel;
    public RectTransform DialogueParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            RectTransform panel = Instantiate(UIPanel);
            panel.transform.SetParent(DialogueParent, false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStrings : MonoBehaviour
{
    [SerializeField]
    private GameObject stringPrefab;
    [SerializeField]
    List<float> anxietyReductions = new List<float>();

    bool FLAG_Reductions;

    public event Action<float> UpdateReductions;

    // Start is called before the first frame update
    void Start()
    {
        FindFirstObjectByType<Player>().MakeString += MakeString;
        FLAG_Reductions = false;
 
    }

    // Update is called once per frame
    void Update()
    {
        if(FLAG_Reductions)
        {
            float sum = 0;
            foreach(float AR in anxietyReductions)
            {
                sum += AR;
            }
            UpdateReductions?.Invoke(sum);
        }
    }

    void MakeString(Transform from, Transform to)
    {
        GameObject newString = Instantiate(stringPrefab);
        RelationshipString rString = newString.GetComponent<RelationshipString>();
        rString.ConnectRelationship(from.gameObject, to.gameObject);

        anxietyReductions.Add(rString.GetAnxietyReduction());
        rString.AssignStringNumber(anxietyReductions.Count - 1);
        rString.EnterRange += EnterRange;
        rString.LeaveRange += LeaveRange;
    }

    void EnterRange(int index, float reduction)
    {
        if(index >= anxietyReductions.Count)
        {
            return;
        }

        if (anxietyReductions[index] == reduction) 
        {
            return;
        }

        anxietyReductions[index] = reduction;
        FLAG_Reductions = true;
    }

    void LeaveRange(int index)
    {
        if (index >= anxietyReductions.Count)
        {
            return;
        }

        if (anxietyReductions[index] == 0)
        {
            return;
        }

        anxietyReductions[index] = 0;
        FLAG_Reductions = true;
    }

}

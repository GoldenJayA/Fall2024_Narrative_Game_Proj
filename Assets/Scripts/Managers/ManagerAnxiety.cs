using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnxiety : MonoBehaviour
{
    [SerializeField]
    private float tickRate; //Number of times per second.
    [SerializeField]
    AnxietyBar anxiety;
    

    private float timer;
    private float baseValue;
    private float currReductions; 

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        baseValue = 3;          //Base amount of anxiety  always being added.
        currReductions = 0;     //Sum of all RelationshipStrings' anxiety reductions (or additions).
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f / tickRate) //Tickrate determines how fast anxiety updates.
        {
            timer -= 1.0f / tickRate;
            currReductions = FindFirstObjectByType<ManagerStrings>().GetAnxiety();
            anxiety.AddAnxiety(baseValue + currReductions);
        }
    }


}

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
    

    private float timer, currTick;
    private float baseValue;
    private float currReductions;
    bool startTicking;

    public event Action AnxietyLoss;

    // Start is called before the first frame update
    void Start()
    {
        currTick = 0;
        timer = 0;
        baseValue = 3;          //Base amount of anxiety  always being added.
        currReductions = 0;     //Sum of all RelationshipStrings' anxiety reductions (or additions).
        startTicking = false;
        FindFirstObjectByType<ManagerSequence>().HouseInitialize += HouseStart;
        FindFirstObjectByType<ManagerInk>().StartTalking += StopCount;
        FindFirstObjectByType<ManagerInk>().FinishedTalking += StartCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTicking && currTick > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f / currTick) //Tickrate determines how fast anxiety updates.
            {
                bool maxedAnxiety = false;
                timer -= 1.0f / currTick;
                currReductions = FindFirstObjectByType<ManagerStrings>().GetAnxiety();
                if (currReductions < 0)
                {
                    maxedAnxiety = anxiety.AddAnxiety(currReductions);
                }
                else
                {
                    maxedAnxiety = anxiety.AddAnxiety(baseValue);
                }

                if (maxedAnxiety)
                {
                    AnxietyLoss?.Invoke();
                }
            }
        }
    }

    void HouseStart()
    {
        startTicking = true;
        currTick = tickRate;
    }

    void StopCount()
    {
        currTick = 0;
    }

    void StartCount()
    {
        currTick = tickRate;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    [SerializeField]
    TMP_Text UITimer;

    [SerializeField]
    float timeScale; //Conversion of seconds to game time minutes/hours.

    float scaledTime, currScale;
    float minutes;
    float hours;
    bool startCounting;

    public event Action<float, float> TimeUpdate; //Hours, Minutes
    public event Action Midnight;


    // Start is called before the first frame update
    void Start()
    {
        currScale = timeScale;
        //timeScale = 60; //1 Second = 1 Minute.
        minutes = 0;
        hours = 0;
        startCounting = false;
        CalculateMinsHours();
        FindFirstObjectByType<ManagerSequence>().HouseInitialize += HouseStart;
        FindFirstObjectByType<ManagerInk>().StartTalking += StopCount;
        FindFirstObjectByType<ManagerInk>().FinishedTalking += StartCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounting)
        {
            scaledTime += Time.deltaTime * currScale;
            CalculateMinsHours();
            if (hours >= 3) //Starting at 9:00, ending at 12:00.
            {
                Midnight?.Invoke();
            }
        }
    }

    //Converts scaledTime into the minutes & hours of in-game time. Updates UI Timer as well.
    void CalculateMinsHours()
    {
        minutes = Mathf.Floor(scaledTime / 60 % 60);
        
        hours = Mathf.Floor(scaledTime / 60 / 60 % 24);
        UpdateUITimer();
        if(minutes % 2 == 0) //Update once very 2 "minutes".
        {
            TimeUpdate?.Invoke(hours, minutes);
        }
    }

    //Takes the current time in minutes & hours to make a 4-digit time display.
    void UpdateUITimer()
    {
        string mins = "";
        string hrs = "";

        if(minutes < 10)
        {
            mins += "0";
        }
        mins += minutes;
        hrs += hours + 9; //Start at 9 PM. Only need to display that time.

        UITimer.text =  hrs + ":" + mins;
    }

    void HouseStart()
    {
        startCounting = true;
    }
    void StopCount()
    {
        currScale = 0;
    }

    void StartCount()
    {
        currScale = timeScale;
    }
}

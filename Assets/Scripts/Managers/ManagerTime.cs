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

    float scaledTime;
    float minutes;
    float hours;

    // Start is called before the first frame update
    void Start()
    {
        //timeScale = 60; //1 Second = 1 Minute.
        minutes = 0;
        hours = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scaledTime += Time.deltaTime * timeScale;
        CalculateMinsHours();
    }

    void CalculateMinsHours()
    {
        minutes = Mathf.Floor(scaledTime / 60 % 60);
        hours = Mathf.Floor(scaledTime / 60 / 60 % 24);
        UpdateUITimer();
    }

    void UpdateUITimer()
    {
        string mins = "";
        string hrs = "";

        if(minutes < 10)
        {
            mins += "0";
        }
        mins += minutes;

        if(hours < 10)
        {
            hrs += "0";
        }
        hrs += hours;

        UITimer.text =  hrs + ":" + mins;
    }
}

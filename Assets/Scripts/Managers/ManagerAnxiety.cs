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
        baseValue = 3;
        currReductions = 0;
        FindFirstObjectByType<ManagerStrings>().UpdateReductions += UpdateReductions;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f / tickRate)
        {
            timer -= 1.0f / tickRate;
            anxiety.AddAnxiety(baseValue + currReductions);
        }
    }

    private void UpdateReductions(float reduce)
    {
        currReductions = reduce;
    }


}

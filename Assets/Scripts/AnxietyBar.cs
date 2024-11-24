using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{
    [SerializeField]
    private float maxVal, minVal, currVal;
    [SerializeField]
    private Image foreground;

    // Start is called before the first frame update
    void Start()
    {
        minVal = 0;
        maxVal = 100;
        currVal = 5;
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Updates the fill amount of the bar based on currVal.
    private void UpdateBar()
    {
        foreground.fillAmount = currVal / maxVal;
    }

    //Changes currVal and updates the anxiety bar.
    public void AddAnxiety(float amount)
    {
        currVal += amount;
        currVal = Mathf.Clamp(currVal, minVal, maxVal);

        UpdateBar();
    }
}

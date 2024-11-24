using System;
using UnityEngine;

public class RelationshipString : MonoBehaviour
{

    [SerializeField]
    Transform target1, target2;
    [SerializeField]
    LineRenderer lineRend;
    [SerializeField]
    float maxLength;
    [SerializeField]
    float anxietyReduction, currReduction; //Negative = reducing, Positve = increassing.
    [SerializeField]
    float anxietyPool, poolMax, poolIncrease;


    int stringNumber; //Depricated or not needed?
    [SerializeField]
    float stringStrength;
    float maxStrength;
    bool inRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target1 != null && target2 != null)
        {
            UpdateString();
        }

        anxietyReduction = -5;
        currReduction = anxietyReduction;

        poolMax = 100;
        anxietyPool = poolMax;
        poolIncrease = 1.5f;

        inRange = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target1 != null && target2 != null)
        {
            UpdateString();
        }
    }

    //Sets the targets of where the line goes from and towards.
    private void ConnectRelationship(Transform from, Transform towards)
    {
        target1 = from;
        target2 = towards;
    }

    //Sets the string number; Depricated?
    private void AssignStringNumber(int index)
    {
        stringNumber = index;
    }

    //Sets the strength of the string based on the relationship built during dialogue.
    private void AssignStringStrength(float max, float curr)
    {
        maxStrength = max;
        stringStrength = curr;

        //Adjust anxiety pool values based on strength?
    }

    //Public creation function, taking in parameters required to make the RelationshipString.
    public void CreateFullString(Transform from, Transform towards, int stringNum, float maxStr, float str)
    {
        AssignStringNumber(stringNum);
        AssignStringStrength(maxStr, str);
        ConnectRelationship(from, towards);
        UpdateString();
    }

    //Calculates the distance between the 2 targets and adjusts various varables due to that.
    private void UpdateString()
    {
        float distance = Vector3.Distance(target1.position, target2.position);
        Vector3 direction = target2.position - target1.position;

        //There's a cylinder that can point along the connection if collision is desired; Scaling is broken.
        transform.position = target1.position + (direction / 2);
        transform.LookAt(target2);
        transform.localScale = new Vector3(1, distance, 1);
        transform.eulerAngles += new Vector3(90, 0, 0);

        Vector3 endPoint = target2.position;
        if (distance > maxLength)
        {
            //Out of range, make sure that the line only extends as far as the maximum length.
            inRange = false;
            endPoint = target1.position + (direction.normalized * maxLength);
            currReduction = 0;
        }
        else
        {
            //In range, make sure that anxiety reduction can happen.
            inRange = true;
            currReduction = anxietyReduction;
        }

        Vector3[] linePoints = { target1.position, endPoint };
        lineRend.SetPositions(linePoints);

        //Width of the string is depentent on the strength of the relationship, which can change over time.
        float stringWidth = 1;
        if (maxStrength != 0)
            stringWidth = stringStrength / maxStrength;
        lineRend.startWidth = stringWidth;
        lineRend.endWidth = stringWidth;
        
        UpdateColor(distance);
    }

    //Updates the color of the string to match physical distance & anxiety pool status.
    private void UpdateColor(float currLength)
    {
        float hue, saturation, lightness;
        Color.RGBToHSV(Color.green, out hue, out saturation, out lightness);

        saturation = Mathf.Min((anxietyPool / poolMax) * 5, 1); //Become closer to gray if pool is depleted.
        lightness = Mathf.Min(0.5f, (maxLength - currLength) / maxLength * 10); //Lose all color once out of range. (Subject to change).

        Color lineColor = Color.HSVToRGB(hue, saturation, lightness);

        lineRend.startColor = lineColor;
        lineRend.endColor = lineColor;

        //Debug.Log("Current Line Color: " + lineColor + " from HSL values: " + hue + " " + saturation + " " + lightness);

    }

    //Adjusts the Anxiety Pool based on numerous factors, but mainly if the player is in range.
    public float AnxietyTick()
    {
        float retVal = 0;
        if (!inRange) //Not in range, refill the pool.
        {
            anxietyPool = Mathf.Clamp(anxietyPool + poolIncrease, 0, poolMax);
        }
        else if (inRange && anxietyPool > 0 && currReduction < 0) //In range & reducing, remove from pool.
        {
            if(anxietyPool + currReduction > 0)
            {
                anxietyPool += currReduction;
                retVal = currReduction;
            }
            else
            {
                anxietyPool = 0;
                retVal = -anxietyPool;
            }
        }
        else if(inRange && currReduction > 0) //In rnage and increasing. Ignore pool.
        {
            retVal = currReduction;
        }

        return retVal;
    }
}

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
    float anxietyReduction; //Negative = reducing, Positve = increassing.


    int stringNumber;
    [SerializeField]
    float stringStrength;
    float maxStrength;

    public event Action<int> LeaveRange; //StringNumber
    public event Action<int, float> EnterRange; //StringNumber, anxietyReduction


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target1 != null && target2 != null)
        {
            UpdateString();
        }
        anxietyReduction = -5;
    }

    // Update is called once per frame
    void Update()
    {
        if (target1 != null && target2 != null)
        {
            UpdateString();
        }
    }

    private void ConnectRelationship(Transform from, Transform towards)
    {
        target1 = from;
        target2 = towards;
    }

    private void AssignStringNumber(int index)
    {
        stringNumber = index;
    }

    private void AssignStringStrength(float max, float curr)
    {
        maxStrength = max;
        stringStrength = curr;
    }

    public void CreateFullString(Transform from, Transform towards, int stringNum, float maxStr, float str)
    {
        AssignStringNumber(stringNum);
        AssignStringStrength(maxStr, str);
        ConnectRelationship(from, towards);
        UpdateString();
    }

    private void UpdateString()
    {
        float distance = Vector3.Distance(target1.position, target2.position);
        Vector3 direction = target2.position - target1.position;

        transform.position = target1.position + (direction / 2);
        transform.LookAt(target2);
        transform.localScale = new Vector3(1, distance, 1);
        transform.eulerAngles += new Vector3(90, 0, 0);

        Vector3 endPoint = target2.position;
        if (distance > maxLength)
        {
            endPoint = target1.position + (direction.normalized * maxLength);
            LeaveRange?.Invoke(stringNumber);
        }
        else
        {
            EnterRange?.Invoke(stringNumber, anxietyReduction);
        }

        LengthColor(distance);
        Vector3[] linePoints = { target1.position, endPoint };
        lineRend.SetPositions(linePoints);

        float stringWidth = 1;
        if (maxStrength != 0)
            stringWidth = stringStrength / maxStrength;
        lineRend.startWidth = stringWidth;
        lineRend.endWidth = stringWidth;
    }

    private void LengthColor(float currLength)
    {
        Color lineColor = Color.white;
        if (currLength / maxLength > 0.5f)
        {
            float factor = 1 - (Mathf.Max(0, (maxLength - currLength)) / maxLength);
            lineColor.g -= factor;
            lineColor.b -= factor;
        }

        lineRend.startColor = lineColor;
        lineRend.endColor = lineColor;

    }

    public float GetAnxietyReduction()
    {
        return anxietyReduction;
    }

    public void SetAnxietyReduction(float newVal)
    {
        anxietyReduction = newVal;
    }
}

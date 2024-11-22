using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Player : Character
{
    [SerializeField]
    private float walkSpeed, turnSpeed;
    [SerializeField]
    Collider interact;

    public event Action<Transform, Transform> MakeString; //"From", "To", StringStrength, MaxStrength.
    public event Action<Story> SelectDialogue; //NPC Game Object

    // Start is called before the first frame update
    void Start()
    {
        interact.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovementControl();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    void MovementControl()
    {
        float forBack = Input.GetAxis("Vertical");
        float spin = Input.GetAxis("Horizontal");

        transform.position += transform.forward * forBack * walkSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, spin * turnSpeed * Time.deltaTime);
    }

    void Interact()
    {
        interact.enabled = true;
        Invoke("TurnOffInteract", 0.1f);
    }

    void TurnOffInteract()
    {
        interact.enabled = false;
    }

    public void TalkToNPC(GameObject NPCGameObject, Story NPCDialogue)
    {
        //Enter Dialogue Tree.
        SelectDialogue?.Invoke(NPCDialogue);
        
        //Make the String
        MakeString?.Invoke(NPCGameObject.transform, transform);  
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class FirstPersonPlayer : Character
{
    [SerializeField]
    private float walkSpeed, turnSpeed;
    [SerializeField]
    Collider interact;
    [SerializeField]
    private float mouseSens = 100;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject relStringPoint;


    private float xRot = 0;

    public event Action<Transform, Transform> MakeString; //"From", "To", StringStrength, MaxStrength.
    public event Action<TextAsset> SelectDialogue; //NPC Game Object

    private GameObject lastNPCStringPoint;
    private bool isTalking, isSequence;

    // Start is called before the first frame update
    void Start()
    {
        FindFirstObjectByType<ManagerInk>().FinishedTalking += FinishTalking;
        interact.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isTalking = isSequence = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTalking)
        {
            MovementControl();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Interact();
            }
        }
    }

    //Basic 3D First Person character control.
    void MovementControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float forBack = Input.GetAxis("Vertical");
        float leftRight = Input.GetAxis("Horizontal");

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        Vector3 movDir = ((transform.forward * forBack) + (transform.right * leftRight)).normalized * walkSpeed * Time.deltaTime;
        transform.position += movDir;
        transform.Rotate(Vector3.up * mouseX);
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }

    //Activates the hitbox for interacting with NPCs and other objects.
    public void Interact()
    {
        interact.enabled = true;
        Invoke("TurnOffInteract", 0.1f);
    }

    //Deactivates said hitbox.
    void TurnOffInteract()
    {
        interact.enabled = false;
    }

    public void TalkToNPC(GameObject stringPoint, TextAsset NPCDialogue)
    {
        //Enter Dialogue Tree.
        lastNPCStringPoint = stringPoint;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isTalking = true;
        SelectDialogue?.Invoke(NPCDialogue);
    }

    public void TalkSequence(TextAsset sequence)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isTalking = true;
        isSequence = true;
        SelectDialogue?.Invoke(sequence);
    }

    private void FinishTalking()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isTalking = false;
        //Make the String
        if (!isSequence)
        { 
            MakeString?.Invoke(lastNPCStringPoint.transform, relStringPoint.transform);
        }
    }

}

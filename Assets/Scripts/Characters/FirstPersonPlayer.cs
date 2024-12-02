using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

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

    private float xRot = 0;

    public event Action<Transform, Transform> MakeString; //"From", "To", StringStrength, MaxStrength.
    public event Action<TextAsset> SelectDialogue; //NPC Game Object

    // Start is called before the first frame update
    void Start()
    {
        interact.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    //Simple tank controls for moving around. Tank controols = forward & backward to move, left & right to turn.
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
    void Interact()
    {
        interact.enabled = true;
        Invoke("TurnOffInteract", 0.1f);
    }

    //Deactivates said hitbox.
    void TurnOffInteract()
    {
        interact.enabled = false;
    }

    public void TalkToNPC(GameObject NPCGameObject, TextAsset NPCDialogue)
    {
        //Enter Dialogue Tree.
        SelectDialogue?.Invoke(NPCDialogue);
        
        //Make the String
        MakeString?.Invoke(NPCGameObject.transform, transform);  
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerController : MonoBehaviour
{
    public static event Action<GameObject> OnInteractNPC;
    public float moveSpeed = 5f; // Speed of movement
    private Vector3 moveDirection; // Direction of movement
    public GameObject selectedNPC;
    private bool nearNPC;

    void Update()
    {
        // Get input from ASWD keys
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys

        // Create a movement direction vector
        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move the character
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (nearNPC && Input.GetKeyDown(KeyCode.E))
        {
            OnInteractNPC(selectedNPC);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        nearNPC = true;
        selectedNPC = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        nearNPC = false;
    }
}

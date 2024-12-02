using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioManager : MonoBehaviour
{
    public AudioClip[] footstepClips; // Array of footstep sounds for the tile surface
    public AudioSource audioSource; // Audio source for playing footstep sounds
    public float stepInterval = 0.5f; // Time interval between footsteps

    private float stepTimer = 0f; // Timer to handle step intervals

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is missing. Please attach or assign one!");
            }
        }
    }

    void Update()
    {
        if (IsMovingForwardOrBackward()) // Check if the character is moving forward or backward
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer if character stops
        }
    }

    private bool IsMovingForwardOrBackward()
    {
        // Check if the player is pressing forward or backward movement keys
        float verticalInput = Input.GetAxis("Vertical");
        return Mathf.Abs(verticalInput) > 0.1f; // Threshold to ignore very small input
    }

    private void PlayFootstepSound()
    {
        if (footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No footstep sounds assigned!");
        }
    }
}

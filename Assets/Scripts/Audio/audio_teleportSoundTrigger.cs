using UnityEngine;

public class TeleportSoundTrigger : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource to play the sound
    public AudioClip teleportSound; // The teleport sound effect

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && teleportSound != null)
            {
                // Assign the teleport sound and enable looping
                audioSource.clip = teleportSound;
                audioSource.loop = true;

                // Start playing the sound if not already playing
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("AudioSource or teleportSound not assigned!");
            }
        }
    }
}
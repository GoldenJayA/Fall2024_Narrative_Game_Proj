using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public ObjectCreationManager objectCreationManager; // Reference to the object creation script
    public AudioSource audioSource;                    // Audio source to play sound
    public AudioClip spawnSound;                       // Sound effect for spawning

    private void OnEnable()
    {
        // Subscribe to the event
        if (objectCreationManager != null)
        {
            objectCreationManager.OnObjectCreated.AddListener(PlaySpawnSound);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (objectCreationManager != null)
        {
            objectCreationManager.OnObjectCreated.RemoveListener(PlaySpawnSound);
        }
    }

    private void PlaySpawnSound()
    {
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
            Debug.Log("Spawn sound played.");
        }
        else
        {
            Debug.LogWarning("AudioSource or spawnSound is not set.");
        }
    }
}
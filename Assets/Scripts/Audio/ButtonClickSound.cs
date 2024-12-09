using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource to play sounds
    public AudioClip clickSound;    // The sound effect for button clicks

    // This method will be linked in the OnClick() event
    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or clickSound is not assigned!");
        }
    }
}

using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource1; // First AudioSource
    public AudioSource audioSource2; // Second AudioSource
    public AudioClip[] musicTracks; // Array of music tracks
    public float fadeDuration = 2.0f; // Time for fading in/out

    private int lastPlayedIndex = -1; // Index of the last played track
    private bool isPlayingTrack1 = true; // Tracks which AudioSource is currently active

    void Start()
    {
        if (musicTracks.Length > 0)
        {
            PlayNextTrack();
        }
        else
        {
            Debug.LogWarning("No music tracks assigned!");
        }
    }

    public void PlayNextTrack()
    {
        int nextTrackIndex = GetNextTrackIndex();
        AudioClip nextTrack = musicTracks[nextTrackIndex];

        StartCoroutine(FadeTracks(nextTrack));
    }

    private int GetNextTrackIndex()
    {
        int nextIndex;
        do
        {
            nextIndex = Random.Range(0, musicTracks.Length);
        } while (nextIndex == lastPlayedIndex);

        lastPlayedIndex = nextIndex;
        return nextIndex;
    }

private IEnumerator FadeTracks(AudioClip nextTrack)
    {
        AudioSource fadingOutSource = isPlayingTrack1 ? audioSource1 : audioSource2;
        AudioSource fadingInSource = isPlayingTrack1 ? audioSource2 : audioSource1;

        // Set up the new track for the fading-in AudioSource
        fadingInSource.clip = nextTrack;
        fadingInSource.volume = 0f;
        fadingInSource.Play();

        float timer = 0f;

        // Perform the fade-out and fade-in simultaneously
        while (timer < fadeDuration)
        {
        timer += Time.deltaTime;
        float t = timer / fadeDuration;

        fadingOutSource.volume = Mathf.Lerp(1f, 0f, t); // Fade out
        fadingInSource.volume = Mathf.Lerp(0f, 1f, t); // Fade in

        yield return null;
        }

        // Finalize the fade
        fadingOutSource.volume = 0f;
        fadingOutSource.Stop();
        fadingInSource.volume = 1f;

        // Toggle which AudioSource is active
        isPlayingTrack1 = !isPlayingTrack1;

        // Automatically play the next track after this one finishes
        Invoke("PlayNextTrack", fadingInSource.clip.length);
    }

}
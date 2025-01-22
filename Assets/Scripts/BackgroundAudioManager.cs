using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour
{
    public static BackgroundAudioManager Instance; // Singleton instance
    private AudioSource audioSource;              // AudioSource to play the background audio

    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes

            // Add an AudioSource component to this GameObject if it doesn't exist
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void PlayBackgroundAudio(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (audioSource.isPlaying) return; // Prevent restarting the audio if already playing

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopBackgroundAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

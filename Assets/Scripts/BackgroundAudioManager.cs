using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour
{
    public static BackgroundAudioManager Instance; // Singleton instance
    private AudioSource audioSource;              // AudioSource to play the background audio

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make it persist across scenes

            // Get or create an AudioSource component
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

    public void PauseBackgroundAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ResumeBackgroundAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}

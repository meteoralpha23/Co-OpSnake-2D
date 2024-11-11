using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton instance
    public static SoundManager Instance;

    // Reference to audio source for playing sounds
    private AudioSource audioSource;

    // Sound Clips
    public AudioClip snakeDeathClip;
    public AudioClip pointTakenClip;
    public AudioClip backgroundMusicClip;

    // Volume for background music (default to 0.5 for mid volume)
    public float backgroundMusicVolume = 0.5f;

    private void Awake()
    {
        // Check if there is already an instance
        if (Instance == null)
        {
            Instance = this; // Assign this to the static instance
            DontDestroyOnLoad(gameObject); // Keep the SoundManager across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        // Get the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Optionally, start playing background music if desired
        if (backgroundMusicClip != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundMusicClip;
            audioSource.volume = backgroundMusicVolume; // Set the background music volume here
            audioSource.Play();
        }
    }

    // Play the Snake Death sound
    public void PlaySnakeDeathSound()
    {
        if (snakeDeathClip != null)
        {
            audioSource.PlayOneShot(snakeDeathClip);
        }
    }

    // Play the Point Taken sound
    public void PlayPointTakenSound()
    {
        if (pointTakenClip != null)
        {
            audioSource.PlayOneShot(pointTakenClip);
        }
    }

    // Optional: Play background music (can be toggled off or on)
    public void ToggleBackgroundMusic(bool isPlaying)
    {
        if (isPlaying)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    // Method to change background music volume
    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicVolume = Mathf.Clamp(volume, 0f, 1f); // Ensure volume stays between 0 and 1
        audioSource.volume = backgroundMusicVolume; // Adjust volume
    }
}

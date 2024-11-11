using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    public AudioClip snakeDeathClip;
    public AudioClip pointTakenClip;
    public AudioClip backgroundMusicClip;

    public float backgroundMusicVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        if (backgroundMusicClip != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundMusicClip;
            audioSource.volume = backgroundMusicVolume;
            audioSource.Play();
        }
    }

    public void PlaySnakeDeathSound()
    {
        if (snakeDeathClip != null)
        {
            audioSource.PlayOneShot(snakeDeathClip);
        }
    }

    public void PlayPointTakenSound()
    {
        if (pointTakenClip != null)
        {
            audioSource.PlayOneShot(pointTakenClip);
        }
    }

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

    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicVolume = Mathf.Clamp(volume, 0f, 1f);
        audioSource.volume = backgroundMusicVolume;
    }
}

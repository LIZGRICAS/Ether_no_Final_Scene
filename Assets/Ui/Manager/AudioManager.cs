using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton Instance

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // AudioSource para música
    [SerializeField] private AudioSource sfxSource;   // AudioSource para efectos de sonido

    private void Awake()
    {
        // Configuración del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Si ya existe una instancia, destruye esta
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Validación de los Sources
        if (sfxSource == null)
        {
            Debug.LogError("sfxSource no está asignado en el Inspector.");
        }
        if (musicSource == null)
        {
            Debug.LogError("musicSource no está asignado en el Inspector.");

        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true; // Música en bucle
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music Source no está asignado en AudioManager.");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else if (clip == null)
        {
            Debug.LogWarning("AudioManager: El AudioClip pasado a PlaySound es null.");
        }
    }
    public void StopSound()
    {
        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(volume);
        }
    }
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp01(volume);
        }
    }
}



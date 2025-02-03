using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip titleTheme;

    void Start()
    {
        AudioManager.Instance.PlayMusic(titleTheme);
    }


    void Update()
    {
        
    }
}

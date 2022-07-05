using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] AudioClips;
    public AudioSource AudioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
}

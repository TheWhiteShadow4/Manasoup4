using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource soundSource;

    public AudioClip[] musicClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ChooseNextTrack();
    }

    private void Update()
    {
        if (musicSource.timeSamples >= musicSource.clip.samples)
        {
            ChooseNextTrack();
        }
    }

    private void ChooseNextTrack()
    {
        var oldTrack = musicSource.clip;
        while (true)
        {
            musicSource.clip = musicClips[Random.Range(0, musicClips.Length - 1)];
            if (musicSource.clip != oldTrack) break;
        }
        musicSource.Play();
    }
}

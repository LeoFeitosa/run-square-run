using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] float timeToFade = 3;
    [SerializeField] AudioClip defaultMusic;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sfxSource.loop = false;
        musicSource.loop = true;
        PlayMusic(defaultMusic);
    }

    public void PlayAudioCue(AudioClip clip)
    {
        sfxSource.pitch = 1;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            StartCoroutine(PlayMusicWithFade(clip));
        }
    }

    IEnumerator PlayMusicWithFade(AudioClip clip)
    {
        if (MusicIsPlaying())
        {
            for (float volume = 1; volume >= 0; volume -= timeToFade)
            {
                yield return new WaitForSeconds(0.1f);
                musicSource.volume = volume;
            }
        }

        musicSource.volume = 0;
        musicSource.clip = clip;
        musicSource.Play();

        for (float volume = 0; volume <= 1; volume += timeToFade)
        {
            yield return new WaitForSeconds(0.1f);
            musicSource.volume = volume;
        }

        musicSource.volume = 1;
    }

    public bool MusicIsPlaying()
    {
        if (musicSource.isPlaying)
        {
            return true;
        }
        return false;
    }
}

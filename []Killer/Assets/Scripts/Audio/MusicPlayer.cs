using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();
    [SerializeField] int musicIndex;
    AudioSource audioSource;

    public AudioMixerGroup masterAudioGroup;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = masterAudioGroup;
        PlayMusic(musicClips[musicIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            musicIndex++;
            if (musicIndex >= musicClips.Count)
                musicIndex = 0;
            PlayMusic(musicClips[musicIndex]);
        } 
    }

    void PlayMusic(AudioClip musicClip)
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }
}

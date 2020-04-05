using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();
    [SerializeField] int musicIndex;
    private float timeOfClip = 0;
    private float time = 0;
    AudioSource audioSource;

    public AudioMixerGroup masterAudioGroup;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = masterAudioGroup;
        PlayMusic(musicClips[musicIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= timeOfClip)
        {
            time = 0;
            musicIndex++;
            if(musicIndex >= musicClips.Count)
                musicIndex = 0;
            PlayMusic(musicClips[musicIndex]);
        }      
    }

    void PlayMusic(AudioClip musicClip)
    {
        audioSource.clip = musicClip;
        timeOfClip = musicClips[musicIndex].length;
        audioSource.Play();
    }
}

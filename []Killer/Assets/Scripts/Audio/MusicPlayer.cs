using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();
    [SerializeField] int musicIndex;
    private float timeOfClip = 0;
    private float time = 0;
    [SerializeField] float volume = 0.7f;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
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
        audioSource.volume = volume;
        timeOfClip = musicClips[musicIndex].length;
        audioSource.Play();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
}

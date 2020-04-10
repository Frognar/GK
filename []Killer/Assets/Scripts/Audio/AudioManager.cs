using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    List<AudioSource> gameThemes = new List<AudioSource>();
    private int gameThemeIndex = 0;
    private int gameThemesCount = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.outputAudioMixerGroup;
            if (s.name.Contains("GameTheme"))
            {
                gameThemesCount++;
                gameThemes.Add(s.source);
            }
        }
    }

    void Start()
    {
        gameThemes[gameThemeIndex].Play();
    }

    void Update()
    {
        if (!gameThemes[gameThemeIndex].isPlaying)
        {
            gameThemeIndex++;

            if (gameThemeIndex >= gameThemesCount)
                gameThemeIndex = 0;
            
            gameThemes[gameThemeIndex].Play();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Play();
    }

}

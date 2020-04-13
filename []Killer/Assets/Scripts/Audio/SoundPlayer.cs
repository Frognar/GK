using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    SoundManager soundManager;

    public string[] soundNames;

    public string[] SoundNames { get { return soundNames; } set { soundNames = value; } }

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void PlaySoundEvent(string soundName)
    {
        soundManager.PlaySound(soundName);
    }

    public void StopSoundEvent(string soundName)
    {
        soundManager.StopSound(soundName);
    }

}

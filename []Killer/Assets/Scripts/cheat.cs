using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class cheat : MonoBehaviour
{
    private Player player;
    private SoundManager soundManager;

    [Header ("Cheats")]
    public float speedHack = 1f;
    public bool godMode = false;

    [Header("Sounds Debug")]
    [Range (0, 1)] public float MainThemeTime = 0f;
    [Range (0, 1)] public float BattleThemeTime = 0f;

    private void Start () {
        player = PlayerManager.instance.player.GetComponent<Player>();
        soundManager = SoundManager.instance;
    }

    void LateUpdate()
    {
        if(Time.timeScale > 0)
            Time.timeScale = speedHack;
        if (godMode) {
            player.Health = 10000;
        }

        if (soundManager.GetSoundEvent(0)?.defaultAudioSource.clip != null)
            MainThemeTime = soundManager.GetSoundEvent (0).defaultAudioSource.time / soundManager.GetSoundEvent (0).defaultAudioSource.clip.length;
        if (soundManager.GetSoundEvent (9)?.defaultAudioSource.clip != null)
            BattleThemeTime = soundManager.GetSoundEvent (9).defaultAudioSource.time / soundManager.GetSoundEvent (9).defaultAudioSource.clip.length;
    }
}

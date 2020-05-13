using System.Collections;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class SoundPlayer : MonoBehaviour {

    SoundManager soundManager;
    AudioSource audioSource;
    Transform player;

    private void Start () {
        soundManager = SoundManager.instance;
        audioSource = GetComponent<AudioSource> ();
        player = PlayerManager.instance.player.transform;
    }

    private void Update () {
        if (audioSource != null) {
            float distance = (transform.position - player.position).magnitude;
            if (distance >= 80f)
                audioSource.volume = 0;
            else
                audioSource.volume = 5 / distance;
        }
    }

    // If GameObject have audio source, use it instead of the default audio source
    public void PlaySoundEvent (string soundName) {
        if (audioSource != null)
            soundManager.PlaySoundOnSpecifiedSource (soundName, audioSource);
        else
            soundManager.PlaySoundOnDefaultSource (soundName);
    }

    public void StopSoundEvent (string soundName) {
        if (audioSource != null)
            soundManager.StopSoundOnSpecifiedSource (soundName, audioSource);
        else
            soundManager.StopSoundOnDefaultSource (soundName);
    }

    // Force using default audio source
    public void PlaySoundEventOnDefaultSource (string soundName) {
        soundManager.PlaySoundOnDefaultSource (soundName);
    }

    public void StopSoundEventOnDefaultSource (string soundName) {
        soundManager.StopSoundOnDefaultSource (soundName);
    }
}

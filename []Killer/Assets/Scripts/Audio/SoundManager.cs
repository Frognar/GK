using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[System.Serializable]
public class SoundEvent {

    public string name;
    public AudioSource defaultAudioSource;
    public AudioClip[] audioClips;
    public AudioMixerGroup output;
    [Range (0f, 1f)] public float minVolume = 1f;
    [Range (0f, 1f)] public float maxVolume = 1f;
    [Range (0f, 3f)] public float minPitch = 1f;
    [Range (0f, 3f)] public float maxPitch = 1f;
    [Range (-1f, 1f)] public float stereoPan = 0f;
    public bool avoidRepeat = false;
    [Tooltip ("All audio clips will be played in a loop")]
    public bool randomizeLoop = false;
    [Tooltip ("Only one audio clip will be played in the loop")]
    public bool loop = false;
    public bool mute = false;
    public bool playOnAwake = false;
    public Coroutine coroutine = null;

    private void Play (AudioSource source) {
        float randomVolume = Random.Range (minVolume, maxVolume);
        float randomPitch = Random.Range (minPitch, maxPitch);

        source.volume = randomVolume;
        source.pitch = randomPitch;
        source.panStereo = stereoPan;
        source.loop = loop;
        source.mute = mute;
        source.outputAudioMixerGroup = output;

        if (!avoidRepeat) {
            if (audioClips.Length > 0) {
                source.clip = audioClips[Random.Range (0, audioClips.Length)];
                source.Play ();
            }
        } else {
            if (audioClips.Length > 0) {
                int r = Random.Range (1, audioClips.Length);

                if (audioClips.Length == 1)
                    r = 0;

                source.clip = audioClips[r];
                source.Play ();
                audioClips[r] = audioClips[0];
                audioClips[r] = source.clip;
            }
        }
    }

    public void PlayOnSpecifiedSource (AudioSource audioSource) {
        Play (audioSource);
    }

    public void PlayOnDefaultSource () {
        Play (this.defaultAudioSource);
    }

    private void Stop (AudioSource source) {
        source.Stop ();
    }

    public void StopDefaultSource () {
        Stop (this.defaultAudioSource);
    }

    public void StopSpecifiedSource (AudioSource audioSource) {
        Stop (audioSource);
    }

    private bool IsPlaying (AudioSource source) {
        return source.isPlaying;
    }

    public bool IsPlayingOnDefaultSource () {
        return IsPlaying(this.defaultAudioSource);
    }

    public bool IsPlayingOnSpecifiedSource (AudioSource audioSource) {
        return IsPlaying (audioSource);
    }
}

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    [SerializeField]
    SoundEvent[] SoundEvents;

    private bool changingSound = false;

    private void Awake () {
        if (instance != null && instance != this)
            Destroy (gameObject);
        else {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }

        for (int i = 0; i < SoundEvents.Length; i++) {
            if (SoundEvents[i].defaultAudioSource == null) {
                GameObject gameObject = new GameObject ("SoundEvent_" + i + "_Default_" + SoundEvents[i].name);
                gameObject.transform.SetParent (this.transform);
                SoundEvents[i].defaultAudioSource = gameObject.AddComponent<AudioSource> ();
            }

            if (SoundEvents[i].playOnAwake)
                PlaySoundOnDefaultSource (SoundEvents[i].name);
        }
    }

    public void PlaySoundOnDefaultSource (string name) {
        SoundEvent s = Array.Find (SoundEvents, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + name + "]");
            return;
        }

        if (s.coroutine != null) {
            StopCoroutine (s.coroutine);
            s.coroutine = null;
        }

        s.PlayOnDefaultSource ();

        if (s.randomizeLoop)
            s.coroutine = StartCoroutine (RepeatPlaySound (s));
    }

    public void PlaySoundOnSpecifiedSource (string name, AudioSource audioSource) {
        SoundEvent s = Array.Find (SoundEvents, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + name + "]");
            return;
        }

        s.PlayOnSpecifiedSource (audioSource);
    }

    public void StopSoundOnDefaultSource (string name) {
        SoundEvent s = Array.Find (SoundEvents, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + name + "]");
            return;
        }

        if (s.coroutine != null) {
            StopCoroutine (s.coroutine);
            s.coroutine = null;
        }
        s.StopDefaultSource ();
    }

    public void StopSoundOnSpecifiedSource (string name, AudioSource audioSource) {
        SoundEvent s = Array.Find (SoundEvents, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + name + "]");
            return;
        }

        s.StopSpecifiedSource (audioSource);
    }

    public bool IsSoundPlaying (string name) {
        SoundEvent s = Array.Find (SoundEvents, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + name + "]");
            return false;
        }

        return s.IsPlayingOnDefaultSource ();
    }

    public void ChangeMusicInitializeCoroutine (string from, string to) {
        StartCoroutine (ChangeMusic (from, to));
    }

    IEnumerator ChangeMusic (string from, string to) {
        if (changingSound)
            StopCoroutine (ChangeMusic (from, to));
        else
            changingSound = true;

        SoundEvent SoundNowPlaying = Array.Find (SoundEvents, sound => sound.name == from);
        SoundEvent SoundToPlay = Array.Find (SoundEvents, sound => sound.name == from);
        if (SoundNowPlaying == null)
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + from + "]");
        else if (SoundToPlay == null)
            Debug.LogWarning ("SoundManager: Sound not found in SoundEvents [" + to + "]");
        else {
            while (SoundNowPlaying.defaultAudioSource.volume > 0f) {
                yield return new WaitForSeconds (0.25f);
                SoundNowPlaying.defaultAudioSource.volume -= 0.01f;
            }

            yield return new WaitForSeconds (0.2f);

            StopSoundOnDefaultSource (from);
            PlaySoundOnDefaultSource (to);
        }
        changingSound = false;
    }

    IEnumerator RepeatPlaySound (SoundEvent soundEvent) {
        yield return new WaitForSeconds (soundEvent.defaultAudioSource.clip.length);
        PlaySoundOnDefaultSource (soundEvent.name);
    }

    public SoundEvent GetSoundEvent (int index) {
        if (SoundEvents.Length >= index + 1)
            return SoundEvents[index];
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    // Static Reference
    public static AudioManager instance;

    // Class and Structs and enums
    [System.Serializable]
    public struct Clip
    {
        public string name;
        public AudioClip audioClip;
    }

    public enum PlayType { Effects, SilentEffects };

    // Public Variables
    public Clip[] clips;
    public AudioMixer musicMixer, effectsMixer, silentEffectsMixer;
    public AudioSource musicSource, effectsSource, silentEffectsSource;

    public string musicClip;
    public float musicSwitchSpeed = 100;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SwitchMusic(musicClip);
    }

    public void PlayAudio(PlayType type, string clipName)
    {
        AudioSource audioSourceToPlay = GetSource(type);
        AudioClip audioClipToPlay = GetClip(clipName);

        // Check the clip and source are NOT null, if they aren't cancel.
        if (audioClipToPlay == null || audioSourceToPlay == null)
            return;

        // Play one shot
        audioSourceToPlay.PlayOneShot(audioClipToPlay);
    }

    private AudioSource GetSource(PlayType type)
    {
        // Find the correct source
        switch (type)
        {
            case PlayType.Effects:
                return effectsSource;
            case PlayType.SilentEffects:
                return silentEffectsSource;
        }

        return null;
    }

    private AudioClip GetClip(string clip)
    {
        // Search for the clip
        for (int i = 0; i < clips.Length; i++)
        {
            // Check the clip name matches
            if (clips[i].name == clip)
            {
                return clips[i].audioClip;
            }
        }

        return null;
    }

    public void SwitchMusic(string newSong)
    {
        musicClip = newSong;
        StartCoroutine(SwitchMusicCoroutine());
    }

    IEnumerator SwitchMusicCoroutine()
    {
        // Set begin volume
        float curVolume;
        musicMixer.GetFloat("Volume", out curVolume);
        // Contain a reference to our original volume
        float orginalVolume = curVolume;

        // Decrease our volume of the music so we don't hear it change songs.
        while(curVolume > -80.0f)
        {
            curVolume -= musicSwitchSpeed * Time.deltaTime;
            musicMixer.SetFloat("Volume", curVolume);
            // Tell unity its okay to go to the next frame now, otherwise its going to keep doing this while loop in this frame and not let us go to the next one. 
            yield return null;
        }

        // Swap song
        musicSource.clip = GetClip(musicClip);
        musicSource.Play();

        // Increase our volume of the music so we hear the new song.
        while (curVolume < orginalVolume)
        {
            curVolume += musicSwitchSpeed * Time.deltaTime;
            // Clamp cur volume to the original so we don't ever go over
            curVolume = Mathf.Clamp(curVolume, -80, orginalVolume);
            musicMixer.SetFloat("Volume", curVolume);
            // Tell unity its okay to go to the next frame now, otherwise its going to keep doing this while loop in this frame and not let us go to the next one. 
            yield return null;
        }
    }
}

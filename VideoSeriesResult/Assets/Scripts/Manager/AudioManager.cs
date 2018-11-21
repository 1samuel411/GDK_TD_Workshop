using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioSource quietEffectsSource;

    public enum Effects { music, effects, quiet };

    public float musicChangeSpeed;
    
    [System.Serializable]
    public struct Clip
    {
        public string name;
        public AudioClip clip;
    }
    public Clip[] clips;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;

        ChangeMusic("Music");
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PlayClip(Effects effect, string clip)
    {
        Clip clipToPlay = FindClip(clip);

        switch(effect)
        {
            case Effects.effects:
                effectsSource.PlayOneShot(clipToPlay.clip);
                break;
            case Effects.quiet:
                quietEffectsSource.PlayOneShot(clipToPlay.clip);
                break;
        }
    }

    public void ChangeMusic(string song)
    {
        StartCoroutine(ChangeMusicCoroutine(song));
    }

    IEnumerator ChangeMusicCoroutine(string song)
    {
        Clip clipToPlay = FindClip(song);

        float originalVolume;
        float curVolume;
        musicSource.outputAudioMixerGroup.audioMixer.GetFloat("Volume", out originalVolume);
        curVolume = originalVolume;

        // Decrease volume
        while(curVolume > -80)
        {
            curVolume -= musicChangeSpeed * Time.deltaTime;
            musicSource.outputAudioMixerGroup.audioMixer.SetFloat("Volume", curVolume);
            yield return null;
        }

        // Volume = 0
        musicSource.clip = clipToPlay.clip;
        musicSource.Play();

        // Increase volume
        while(curVolume < originalVolume)
        {
            curVolume += musicChangeSpeed * Time.deltaTime;
            musicSource.outputAudioMixerGroup.audioMixer.SetFloat("Volume", curVolume);
            yield return null;
        }
    }

    Clip FindClip(string name)
    {
        Clip clip = new Clip();
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == name)
            {
                clip = clips[i];
            }
        }
        return clip;
    }
}

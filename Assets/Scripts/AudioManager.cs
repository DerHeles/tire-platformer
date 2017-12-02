using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip bottle;
    [SerializeField] private AudioClip brokenGlassClip;
    [SerializeField] private AudioClip brokenTV;
    [SerializeField] private AudioClip bubble;
    [SerializeField] private AudioClip dead;
    [SerializeField] private AudioClip doorOpened;
    [SerializeField] private AudioClip doorRammed;
    [SerializeField] private AudioClip fireGhost;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip losePatch;
    [SerializeField] private AudioClip openShelf;
    [SerializeField] private AudioClip pickupKey;
    [SerializeField] private AudioClip pickupPatch;
    [SerializeField] private AudioClip plantGrown;
    [SerializeField] private AudioClip puddle;
    [SerializeField] private AudioClip spikes;
    [SerializeField] private AudioClip skeletonArm;

    private AudioSource[] audioSources;
    [SerializeField] AudioSource musicFriendly;
    [SerializeField] AudioSource musicEvil;
    [SerializeField] AudioSource musicEnd;
    [SerializeField] AudioSource musicTitle;

    [SerializeField] private AudioMixerGroup mixerMusic;
    [SerializeField] private AudioMixerGroup mixerSFX;

    private float adjustedMusicVolume = 0.3f;

    public enum SoundID
    {
        Bottle, BrokenGlass, BrokenTV, Bubble, Dead, DoorOpened, DoorRammed, FireGhost, Jump, LosePatch, OpenShelf, PickupKey, PickupPatch, PlantGrown, Puddle, Spikes, SkeletonArm, Num
    }
    public enum MusicID
    {
        Friendly, Evil, End, Num
    }

    public Dictionary<SoundID, AudioSource> sources;

    private void Start()
    {
        sources = new Dictionary<SoundID, AudioSource>();
        foreach (SoundID id in Enum.GetValues(typeof(SoundID)))
        {
            sources.Add(id, gameObject.AddComponent<AudioSource>());
        }
        sources[SoundID.Bottle].clip = bottle;
        sources[SoundID.BrokenGlass].clip = brokenGlassClip;
        sources[SoundID.BrokenTV].clip = brokenTV;
        sources[SoundID.Bubble].clip = bubble;
        sources[SoundID.Dead].clip = dead;
        sources[SoundID.DoorOpened].clip = doorOpened;
        sources[SoundID.DoorRammed].clip = doorRammed;
        sources[SoundID.FireGhost].clip = fireGhost;
        sources[SoundID.Jump].clip = jump;
        sources[SoundID.LosePatch].clip = losePatch;
        sources[SoundID.OpenShelf].clip = openShelf;
        sources[SoundID.PickupKey].clip = pickupKey;
        sources[SoundID.PickupPatch].clip = pickupPatch;
        sources[SoundID.PlantGrown].clip = plantGrown;
        sources[SoundID.Puddle].clip = puddle;
        sources[SoundID.Spikes].clip = spikes;
        sources[SoundID.SkeletonArm].clip = skeletonArm;

        //musicFriendly = gameObject.AddComponent<AudioSource>();
        //musicFriendly.volume = 0.2f;
        //musicFriendly.loop = true;
        //musicFriendly.Play();

        //musicEvil = gameObject.AddComponent<AudioSource>();
        //musicEvil.volume = 0.0f;
        //musicEvil.loop = true;
        //musicEvil.Play();

        foreach (var source in sources)
        {
            source.Value.outputAudioMixerGroup = mixerSFX;
            source.Value.playOnAwake = false;
        }

        // For loading scene from menu
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(SoundID id)
    {
        sources[id].Play();
    }

    public void PlayMusic(MusicID id)
    {
        if (id == MusicID.Evil)
        {
            musicFriendly.volume = 0f;
            musicEvil.volume = adjustedMusicVolume;
            musicEnd.volume = 0f;
        }
        else if (id == MusicID.Friendly)
        {
            musicFriendly.volume = adjustedMusicVolume;
            musicEvil.volume = 0f;
            musicEnd.volume = 0f;
        }
        else if (id == MusicID.End)
        {
            musicFriendly.volume = 0f;
            musicEvil.volume = 0f;
            musicEnd.volume = adjustedMusicVolume;
            musicEnd.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        //Debug.Log("Music Volume = " + volume);
        mixerMusic.audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        //Debug.Log("SFX Volume = " + volume);
        mixerMusic.audioMixer.SetFloat("sfxVolume", volume);
    }

    public void PlayMenuMusic()
    {
        musicFriendly.Stop();
        musicEnd.Stop();
        musicEvil.Stop();
        musicTitle.Play();
    }

    public void PlayIngameMusic()
    {
        musicFriendly.Play();
        musicFriendly.volume = adjustedMusicVolume;
        musicEvil.Play();
        musicEvil.volume = 0f;
        musicEnd.Stop();
        musicEnd.volume = adjustedMusicVolume;
        musicTitle.Stop();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Effects")]
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
    
    [Header("Music")]
    [SerializeField] private AudioSource musicFriendly;
    [SerializeField] private AudioSource musicEvil;
    [SerializeField] private AudioSource musicEnd;
    [SerializeField] private AudioSource musicTitle;
    [SerializeField] private float adjustedMusicVolume = 0.3f;

    [Header("Mixer")]
    [SerializeField] private AudioMixerGroup mixerMusic;
    [SerializeField] private AudioMixerGroup mixerSFX;


    public enum SoundID
    {
        Bottle, BrokenGlass, BrokenTV, Bubble, Dead, DoorOpened, DoorRammed, FireGhost, Jump, LosePatch, OpenShelf, PickupKey, PickupPatch, PlantGrown, Puddle, Spikes, SkeletonArm, Num
    }

    public enum MusicID
    {
        Friendly, Evil, End, Num
    }

    private Dictionary<SoundID, AudioSource> sources;

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

        foreach (var source in sources)
        {
            source.Value.outputAudioMixerGroup = mixerSFX;
            source.Value.playOnAwake = false;
        }

        // For loading main scene from menu
        DontDestroyOnLoad(gameObject);
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
        mixerMusic.audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
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

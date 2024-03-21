using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public List<AudioClip> MusicSource;
    public List<AudioClip> SfxSource;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public AudioSource _audioSourecMusic;
    public AudioSource _audioSourecSfx;
    private AudioClip _clip;

    public void Start()
    {
        PlayMusic(MusicType.RoomMusic);

        GameManager.Instance.TransportToForest += () => PlayMusic(MusicType.ForestMusic);
        GameManager.Instance.TransportToRoom += () => PlayMusic(MusicType.RoomMusic);
    }

    public void PlayMusic(MusicType mt, bool loop = true)
    {
        Debug.Log(mt);
        _clip = MusicSource[(int)mt];
        _audioSourecMusic.clip = _clip;
        _audioSourecMusic.loop = loop;
        _audioSourecMusic.volume = musicVolume;
        _audioSourecMusic.Play();
    }

    public void StopMusic()
    {
        _audioSourecMusic.Stop();
        _audioSourecSfx.Stop();
    }

    public void PlaySFX(SfxType st, bool loop = false)
    {
        _clip = SfxSource[(int)st];
        _audioSourecSfx.clip = _clip;
        _audioSourecSfx.loop = loop;
        _audioSourecSfx.volume = 0.7f * musicVolume;
        _audioSourecSfx.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        _audioSourecMusic.volume = volume;
        _audioSourecSfx.volume = volume;
    }


}
public enum MusicType
{
    ForestMusic = 0,
    RoomMusic = 1,
}

public enum SfxType
{
    Expolode = 0,
    Cut = 1,
    Stir = 2,
    Generate = 3,
    Pour = 4,
}

using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    int ActiveMusic;
    public bool muteMusic = true;
    public bool muted;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        ActiveMusic = PlayerPrefs.GetInt("MusicActive");

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;           
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }
    }

    public void MuteMusic()
    {
        this.muteMusic = !this.muteMusic;
        float num = !this.muteMusic ? 0.308f : 0.0f;
        foreach (Sound song in this.sounds)
            song.source.volume = num;
    }

    public void UnmuteMusic()
    {
        foreach (Sound sound in this.sounds)
        {
            if (sound.name == "Song")
            {
                sound.source.volume = 1.15f;
                break;
            }
        }
    }

    public void Play(string name)
    {
        foreach (Sound sound in this.sounds)
        {
            if (sound.name == name)
            {
                sound.source.Play();
                break;
            }
        }
    }

    public void Stop(string name)
    {
        foreach (Sound sound in this.sounds)
        {
            if (sound.name == name)
            {
                sound.source.Stop();
                break;
            }
        }
    }
}

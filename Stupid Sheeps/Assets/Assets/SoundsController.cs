using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> BackgroundMusics;
    [SerializeField] private List<AudioSource> Sounds;

    private int BackgrouMusicPlaying = 0;

    private void Start()
    {
        bool MusicIsOn = PlayerPrefs.GetString("MusicPlay") == "on";
        bool SoundIsOn = PlayerPrefs.GetString("SoundPlay") == "on";
        SetMusicOn(MusicIsOn);
        SetSoundOn(SoundIsOn);
    }

    public void SwitchBackgroundMusic()
    {
        if(BackgrouMusicPlaying == 0)// dok ne smislim nesto za 4ti zvuk
        {
            BackgrouMusicPlaying++;
        }
        else
        {
            BackgroundMusics[BackgrouMusicPlaying].Stop();
            BackgrouMusicPlaying++;
            BackgroundMusics[BackgrouMusicPlaying].Play();
        }
    }

    public void PlayClick()
    {
        Sounds[0].Play();
    }

    public void PlayEnableEat()
    {
        Sounds[1].Play();
    }

    public void PlayPauseEnabled()
    {
        Sounds[2].Play();
    }

    public void PlayHighScore()
    {
        Sounds[3].Play();
    }

    public void SetSoundOn(bool on)
    {
        foreach (AudioSource sound in Sounds)
        {
            sound.volume = on ? 1f : 0f;
        }
    }

    public void SetMusicOn(bool on)
    {
        foreach(AudioSource sound in BackgroundMusics)
        {
            sound.volume = on ? 1f : 0f;
        }
    }
}

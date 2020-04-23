using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> BackgroundMusics;
    [SerializeField] private List<AudioSource> Sounds;

    private int BackgrouMusicPlaying = 0;

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
}

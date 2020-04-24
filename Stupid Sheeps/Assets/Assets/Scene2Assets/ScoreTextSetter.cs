using UnityEngine;
using UnityEngine.UI;

public class ScoreTextSetter : MonoBehaviour
{
    public Text Score;
    private float score;
    private int counter;

    private AudioSource CountSound;

    void Start()
    {
        CountSound = GetComponent<AudioSource>();
        CountSound.volume = PlayerPrefs.GetString("SoundPlay") == "on" ? 1f : 0f;
        counter = 0;
        score = PlayerPrefs.GetFloat("Score");

        InvokeRepeating("CountScore", 0f, 0.05f);
        CountSound.Play();
    }

    private void CountScore()
    {
        counter++;
        if (Score.text == score.ToString("F0"))
        {
            CountSound.Stop();
            CancelInvoke("CountScore");
            return;
        }
        Score.text = counter.ToString("F0");
    }
}

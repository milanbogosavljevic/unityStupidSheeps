using UnityEngine;
using UnityEngine.UI;

public class ScoreTextSetter : MonoBehaviour
{
    public Text Score;
    private float score;

    void Start()
    {
        score = PlayerPrefs.GetFloat("Score");
        Score.text = score.ToString("F0");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Sheep> allSheeps;
    public List<GameObject> lifeDots; 

    public int lives = 3;
    public int freezeTimeForSheeps = 3;

    public float score = 0f;
    public float highScore = 0f;
    public Text ScoreText;
    public Text HighScoreText;
    private bool countScore = true;

    private void Start()
    {
        if(PlayerPrefs.HasKey("HighScore") == false)
        {
            PlayerPrefs.SetFloat("HighScore", 0f);
        }
        else
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
            HighScoreText.text = highScore.ToString("F0");
        }
    }

    private IEnumerator WaitToUnfreezeSheeps()
    {
        yield return new WaitForSeconds(freezeTimeForSheeps);
        this.moveSheeps(true);
    }

    public void SheepCollideWithSaw()
    {
        this.moveSheeps(false);
        this.lives--;
        if(this.lives > 0)
        {          
            StartCoroutine(WaitToUnfreezeSheeps());
        }
        else
        {
            this.OnGameOver();
        }
        this.HideDot(this.lives);
    }

    private void OnGameOver()
    {
        this.countScore = false;
        if(score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }

    private void moveSheeps(bool move)
    {
        foreach (Sheep sheep in allSheeps)
        {
            sheep.setCanMove(move);
        }
        countScore = move;
    }

    private void HideDot(int ind)
    {
        this.lifeDots[ind].SetActive(false);
    }

    private void ResetDots()
    {
        foreach(GameObject dot in lifeDots)
        {
            dot.SetActive(true);
        }
    }

    private void Update()
    {
        this.updateScores();
    }

    private void updateScores()
    {
        if (countScore == true)
        {
            score += Time.deltaTime;
            ScoreText.text = score.ToString("F0");

            if(score > highScore)
            {
                HighScoreText.text = score.ToString("F0");
            }
        }
    }
}

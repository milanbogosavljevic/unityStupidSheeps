using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Sheep> allSheeps;
    public List<GameObject> lifeDots;
    public List<float> speedLevels;
    [SerializeField] List<string> scoreCheckPoints;

    public int lives = 3;
    public int freezeTimeForSheeps = 3;
    private int startCounter = 3;
    public float score = 0f;
    public float highScore = 0f;

    private string scoreCheckPoint;

    public Text ScoreText;
    public Text HighScoreText;
    public Text StartCounterText;

    private bool countScore = false;

    private void Start()
    {
        this.SetScoreCheckPoint();
        if (PlayerPrefs.HasKey("HighScore") == false)
        {
            PlayerPrefs.SetFloat("HighScore", 0f);
        }
        else
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
            HighScoreText.text = highScore.ToString("F0");
        }

        StartCounterText.text = startCounter.ToString();
        InvokeRepeating("CountDownStartTime", 1, 1F);
    }

    private void SetScoreCheckPoint()
    {
        scoreCheckPoint = scoreCheckPoints[0];
        scoreCheckPoints.RemoveAt(0);
    }

    private void CountDownStartTime()
    {
        startCounter--;
        StartCounterText.text = startCounter.ToString();
        if(startCounter == -1)
        {
            Destroy(StartCounterText);
            CancelInvoke();
            this.moveSheeps(true);
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
        PlayerPrefs.SetFloat("Score", score);
        SceneManager.LoadScene(2);
    }

    private void moveSheeps(bool move)
    {
        foreach (Sheep sheep in allSheeps)
        {
            sheep.setCanMove(move);
        }
        countScore = move;
    }

    private void ChangeSheepsSpeed()
    {
        float newSpeed = speedLevels[0];
        speedLevels.RemoveAt(0);
        foreach (Sheep sheep in allSheeps)
        {
            sheep.SetSpeed(newSpeed);
        }
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

            if (ScoreText.text == scoreCheckPoint)
            {
                this.SetScoreCheckPoint();
                this.ChangeSheepsSpeed();
            }

            if (score > highScore)
            {
                HighScoreText.text = score.ToString("F0");
            }
        }
    }
}

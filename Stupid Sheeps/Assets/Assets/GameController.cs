using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int lives = 3;
    private int startCounter = 3;
    private float score = 0f;
    private float highScore = 0f;
    private float pauseLoadingBarLevel;
    private float pauseSheepLoadingBarLevel;
    private string scoreCheckPoint;
    private bool countScore = false;
    private bool maxSpeedReached = false;
    public bool canPauseSheep = false;

    [SerializeField] private Text ScoreText;
    [SerializeField] private Text HighScoreText;
    [SerializeField] private Text StartCounterText;

    [SerializeField] private List<string> scoreCheckPoints;
    [SerializeField] private List<Sheep> allSheeps;
    [SerializeField] private List<GameObject> lifeDots;
    [SerializeField] private List<float> speedLevels;
    [SerializeField] private List<Saw> allSaws;
    [SerializeField] private List<Button> allButtons;

    [SerializeField] private Image pauseButtonLoadingBar;
    [SerializeField] private Image pauseSheepLoadingBar;

    [SerializeField] int pauseFreezeTime;
    [SerializeField] int pauseSheepFreezeTime;
    [SerializeField] int pauseCredits;
    [SerializeField] Button pauseButton;

    [SerializeField] private Text PauseCreditsText;

    [SerializeField] private SpeedFinger SpeedFinger;

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
        //InvokeRepeating("CountDownStartTime", 1, 1F);
        InvokeRepeating("CountDownStartTime", 0.1f, 0.1F);

        this.pauseLoadingBarLevel = 1f / pauseFreezeTime;
        pauseSheepLoadingBarLevel = 1f / pauseSheepFreezeTime;

        this.UpdatePauseButtonCreditsText();
    }

    private void UpdatePauseButtonCreditsText()
    {
        PauseCreditsText.text = pauseCredits.ToString();
    }

    private void Update()
    {
        this.updateScores();
    }

    private void SetScoreCheckPoint()
    {
        scoreCheckPoint = scoreCheckPoints[0];
        scoreCheckPoints.RemoveAt(0);
        if(scoreCheckPoints.Count == 0)
        {
            this.maxSpeedReached = true;
        }
        SpeedFinger.SetFingerSpeed(scoreCheckPoint);
    }

    private void CountDownStartTime()
    {
        startCounter--;
        StartCounterText.text = startCounter.ToString();
        if(startCounter == -1)
        {
            Destroy(StartCounterText);
            CancelInvoke();
            this.MoveSheeps(true);
            SpeedFinger.RunAnimation(true);
            StartPauseSheepLoadingAnimation();
        }
    }

    private void updateScores()
    {
        if (countScore == true)
        {
            score += Time.deltaTime;
            ScoreText.text = score.ToString("F0");

            if (this.maxSpeedReached == false)
            {
                if (ScoreText.text == scoreCheckPoint)
                {
                    this.SetScoreCheckPoint();
                    this.ChangeSheepsSpeed();
                }
            }


            if (score > highScore)
            {
                HighScoreText.text = score.ToString("F0");
            }
        }
    }

    /*    private IEnumerator WaitToUnfreezeSheeps()
        {
            yield return new WaitForSeconds(5);
            this.MoveSheeps(true);
        }*/

    public void OnParticlesAnimationDone()
    {
        if (this.lives > 0)
        {
            //StartCoroutine(WaitToUnfreezeSheeps());
            this.ReactivateSheep();
            this.MoveSheeps(true);
        }
        else
        {
            this.OnGameOver();
        }
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

    private void DisableAllButtons()
    {
        foreach (Button button in allButtons)
        {
            button.interactable = false;
        }
    }

    private void MoveSheeps(bool move)
    {
        foreach (Sheep sheep in allSheeps)
        {
            sheep.SetCanMove(move);
        }
        countScore = move;
    }

    public void ReactivateSheep()
    {
        foreach (Sheep sheep in allSheeps)
        {
            if(sheep.gameObject.activeSelf == false)
            {
                sheep.gameObject.SetActive(true);
            }
        }
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

    public void SheepCollideWithSaw()
    {
        this.MoveSheeps(false);
        this.lives--;
        this.HideDot(this.lives);
        if(this.lives < 1)
        {
            this.DisableAllButtons();
        }
    }

    private void HideDot(int ind)
    {
        this.lifeDots[ind].SetActive(false);
    }

    public void PauseButtonHandler()
    {
        if(this.pauseCredits > 0)
        {
            pauseButton.interactable = false;
            this.pauseCredits--;
            this.UpdatePauseButtonCreditsText();
            this.PauseSaws(true);
            StartCoroutine(WaitToUnpauseSaw());
            this.StartPauseButtonLoadingAnimation();
        }
    }

    private IEnumerator WaitToUnpauseSaw()
    {
        yield return new WaitForSeconds(this.pauseFreezeTime);
        this.PauseSaws(false);
    }

    private void PauseSaws(bool pause)
    {
        foreach (Saw saw in this.allSaws)
        {
            saw.PauseSaw(pause);
        }
    }

    private void StartPauseButtonLoadingAnimation()
    {
        //pauseButtonLoadingBar.fillAmount = 0;
        InvokeRepeating("AnimatePauseButtonLoadingBar", 0f, 1F);
    }

    private void AnimatePauseButtonLoadingBar()
    {
        if (pauseButtonLoadingBar.fillAmount == 1f)
        {
            pauseButtonLoadingBar.fillAmount = 0;
            CancelInvoke();
            pauseButton.interactable = true;
            return;
        }
        pauseButtonLoadingBar.fillAmount += this.pauseLoadingBarLevel;
    }

    public void StartPauseSheepLoadingAnimation()
    {
        canPauseSheep = false;
        InvokeRepeating("AnimatePauseSheepLoadingBar", 0f, 1F);
    }

    private void AnimatePauseSheepLoadingBar()
    {
        if (pauseSheepLoadingBar.fillAmount == 1f)
        {
            pauseSheepLoadingBar.fillAmount = 0;
            CancelInvoke();
            canPauseSheep = true;
            return;
        }
        pauseSheepLoadingBar.fillAmount += pauseSheepLoadingBarLevel;
    }

    public bool CanPauseSheep()
    {
        return canPauseSheep;
    }
}

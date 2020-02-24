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
    public Text ScoreText;
    private bool countScore = true;


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
            Debug.Log("Game Over");
        }
        this.HideDot(this.lives);
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
        if(countScore == true)
        {
            score += Time.deltaTime;
            ScoreText.text = score.ToString("F0");
        }
    }
}

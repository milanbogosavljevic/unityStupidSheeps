using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Sheep> allSheeps;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(4);
        this.moveSheeps(true);
    }

    public void SheepCollideWithSaw()
    {
        this.moveSheeps(false);
    }

    private void moveSheeps(bool move)
    {
        foreach (Sheep sheep in allSheeps)
        {
            sheep.setCanMove(move);
        }
        StartCoroutine(Waiter());
    }
}

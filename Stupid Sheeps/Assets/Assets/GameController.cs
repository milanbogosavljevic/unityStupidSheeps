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

    public void SheepCollideWithSaw()
    {
        foreach (Sheep sheep in allSheeps)
        {
            sheep.setCanMove(false);
        }
    }
}

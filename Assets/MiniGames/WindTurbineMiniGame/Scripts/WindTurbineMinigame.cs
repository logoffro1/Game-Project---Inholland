using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineMinigame : MiniGameBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void CoordinateLevel()
    {
       
    }
    public override void GameFinish(bool succesful)
    {

        if (succesful)
        {
            this.GameWon();

        }
        else
        {
            this.GameOver();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

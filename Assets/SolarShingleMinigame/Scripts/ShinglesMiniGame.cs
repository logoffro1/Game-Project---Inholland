using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinglesMiniGame : MiniGameBase
{
    private void Start()
    {

        description = "Build two rows to complete the solar panel!\n\nKEYS\nA,D - Move left / right\nS - Drop\nSPACE - Hard Drop\nQ,E - Rotate left / right";
    }
    public void GameFinish(bool succesful)
    {
        
        if (succesful)
            this.GameWon();
        else
            this.GameOver();
    }

}

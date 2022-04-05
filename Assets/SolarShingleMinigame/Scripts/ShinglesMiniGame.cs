using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinglesMiniGame : MiniGameBase
{

public void GameFinish(bool succesful)
    {
        
        if (succesful)
            this.GameWon();
        else
            this.GameOver();
    }
}

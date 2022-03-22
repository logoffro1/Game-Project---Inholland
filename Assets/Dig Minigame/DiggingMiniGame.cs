using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingMiniGame : MiniGameBase
{
    public void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }
}

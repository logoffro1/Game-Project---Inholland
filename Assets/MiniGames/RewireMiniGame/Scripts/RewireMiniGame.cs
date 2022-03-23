using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewireMiniGame : MiniGameBase
{
    public void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }
}

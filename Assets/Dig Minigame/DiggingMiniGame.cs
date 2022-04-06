using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingMiniGame : MiniGameBase
{
    private void Start()
    {
        description = "Dig a hole for the tree!\nRelease the shovel at the right moment\n\nKEYS\nSPACE - Release shovel";
    }
    public void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }
}

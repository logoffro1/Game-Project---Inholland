using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewireMiniGame : MiniGameBase
{
    private void Start()
    {
        description = "Match the wires by color!\n\nClick on the wire on the left side\nDrag to move\nRelease click to connect";
    }
    public void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }
}

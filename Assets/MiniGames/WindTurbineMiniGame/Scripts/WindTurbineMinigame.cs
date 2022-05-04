using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineMinigame : MiniGameBase
{
    private WeldingLine torch;
    void Start()
    {
        SetLocalizedString();
        torch = GetComponentInChildren<WeldingLine>();
    }
    public override void CoordinateLevel()
    {

    }
    public override void GameFinish(bool succesful)
    {
        torch.isStarted = false;
        if (succesful)
        {
            this.GameWon();
        }
        else
        {
            this.GameOver();
        }
    }
}

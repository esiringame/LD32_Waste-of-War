using UnityEngine;
using System.Collections;

public class VictoryGameState : GameState
{
    public VictoryGameState(GameManager gameManager)
        : base(gameManager)
    {
    }

    public override void Init()
    {
        GameManager.PauseChrono();
    }

    public override void Update()
    {
    }
}

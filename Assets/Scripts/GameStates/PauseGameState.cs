using UnityEngine;
using System.Collections;

public class PauseGameState : GameState
{
    public PauseGameState(GameManager gameManager)
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

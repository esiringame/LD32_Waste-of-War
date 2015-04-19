using UnityEngine;
using System.Collections;

public class IntroGameState : GameState
{
    public IntroGameState(GameManager gameManager)
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

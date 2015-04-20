using UnityEngine;
using System.Collections;

public class BeginGameState : GameState
{
    public BeginGameState(GameManager gameManager)
        : base(gameManager)
    {
    }

    public override void Init()
    {
        GameManager.Pause();

        GameManager.Player.Reset();
    }

    public override void Update()
    {
        GameManager.DifferedChangeState(new IntroGameState(GameManager));
    }
}

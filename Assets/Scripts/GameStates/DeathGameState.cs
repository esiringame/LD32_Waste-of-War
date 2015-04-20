using UnityEngine;
using System.Collections;

public class DeathGameState : GameState
{
    public DeathGameState(GameManager gameManager)
        : base(gameManager)
    {
    }

    public override void Init()
    {
        GameManager.Pause();

        if (GameManager.Player.IsGameOver())
            GameManager.DifferedChangeState(new GameOverState(GameManager));
    }

    public override void Update()
    {
        GameManager.DifferedChangeState(new BeginGameState(GameManager));
    }
}

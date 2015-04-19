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
        GameManager.Resume();
    }

    public override void Update()
    {
    }
}

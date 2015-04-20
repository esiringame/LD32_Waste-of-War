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
    }

    public override void Update()
    {
    }
}

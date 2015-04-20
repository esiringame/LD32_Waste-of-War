using UnityEngine;
using System.Collections;

public class PlayGameState : GameState
{
    public PlayGameState(GameManager gameManager)
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

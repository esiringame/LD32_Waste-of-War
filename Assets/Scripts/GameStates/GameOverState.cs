using UnityEngine;
using System.Collections;

public class GameOverState : GameState
{
    public GameOverState(GameManager gameManager)
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

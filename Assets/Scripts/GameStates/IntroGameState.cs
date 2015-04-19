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
    }

    public override void Update()
    {
        //if (isEnd)
        //    GameManager.ChangeState(new PlayGameState(GameManager));
    }

    public override void End()
    {
    }
}

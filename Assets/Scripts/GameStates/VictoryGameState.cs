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
        GameManager.Pause();
    }

    public override void Update()
    {
        Application.LoadLevel((int)SceneId.HighScore);
    }
}

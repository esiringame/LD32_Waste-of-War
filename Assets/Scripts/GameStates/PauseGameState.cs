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
        GameManager.Pause();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.DifferedChangeState(new PlayGameState(GameManager));
    }
}

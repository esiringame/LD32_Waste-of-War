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
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Player.IsMoving)
            GameManager.DifferedChangeState(new PauseGameState(GameManager.Instance));
    }
}

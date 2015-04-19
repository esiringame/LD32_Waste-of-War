using UnityEngine;
using System.Collections;

public abstract class GameState
{
    protected readonly GameManagerBis GameManager;

    protected GameState(GameManagerBis gameManager)
    {
        GameManager = gameManager;
    }

    public abstract void Init();
    public abstract void Update();
    public abstract void End();
}

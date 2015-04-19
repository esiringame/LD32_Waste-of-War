using UnityEngine;
using System.Collections;

public abstract class GameState
{
    protected readonly GameManager GameManager;

    protected GameState(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    public abstract void Init();
    public abstract void Update();
    public abstract void End();
}

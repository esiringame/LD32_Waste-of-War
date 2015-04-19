using UnityEngine;
using System.Collections;

public class GameManagerBis : MonoBehaviour
{
    public GameState State;
    public PlayerController Player { get; private set; }

    void Awake()
    {
        Player = GetComponentInChildren<PlayerController>();
    }

    void Start()
    {
        State.Init();
    }

    void Update()
    {
        State.Update();
    }

    public void ChangeState(GameState newState)
    {
        State.End();
        State = newState;
        State.Init();
    }
}

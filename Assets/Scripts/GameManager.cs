using System;
using UnityEngine;
using System.Collections;

public class GameManager : DesignPattern.Singleton<GameManager>
{
    public GameState State;
    public PlayerController Player { get; private set; }

    public TimeSpan Chronometer
    {
        get { return TimeSpan.FromSeconds(_chronometer); }
    }

    private float _chronometer;
    private bool _chronometerEnabled = false;

    private bool _differedChangeStateRequest;
    private GameState _stateRequested;

    void Awake()
    {
        State = new BeginGameState(this);

        Player = GetComponentInChildren<PlayerController>();
    }

    void Start()
    {
        State.Init();
    }

    void Update()
    {
        State.Update();

        if (_chronometerEnabled)
            _chronometer += Time.unscaledDeltaTime;

        if (_differedChangeStateRequest)
        {
            ChangeState(_stateRequested);
            _differedChangeStateRequest = false;
        }
    }

    public void Resume()
    {
        _chronometerEnabled = true;
        Player.ControlEnabled = true;
    }

    public void Pause()
    {
        _chronometerEnabled = false;
        Player.ControlEnabled = false;
    }

    public void ResetChrono()
    {
        _chronometer = 0;
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        State.Init();
    }
    
    public void DifferedChangeState(GameState newState)
    {
        _stateRequested = newState;
        _differedChangeStateRequest = true;
    }
}

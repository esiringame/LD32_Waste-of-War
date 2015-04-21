using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : DesignPattern.Singleton<GameManager>
{
    public GameState State;
    public PlayerController Player { get; private set; }
    public ThrowUI ThrowUI { get; private set; }

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
        ThrowUI = GetComponentInChildren<ThrowUI>();
    }

    void Start()
    {
        State.Init();
		ResetChrono ();
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
        ThrowUI.enabled = true;
        GameUIManager.Instance.PauseBackground.GetComponent<Image>().enabled = false;
    }

    public void Pause()
    {
        _chronometerEnabled = false;
        Player.ControlEnabled = false;
        ThrowUI.enabled = false;
        GameUIManager.Instance.PauseBackground.GetComponent<Image>().enabled = true;
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

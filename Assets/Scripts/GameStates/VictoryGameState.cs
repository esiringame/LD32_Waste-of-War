using UnityEngine;
using System.Collections;
using System;

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
		GameObject.Find ("Score").SendMessage("setScore", GameManager.Instance.Chronometer);

		//GameManager.ResetChrono ();
		//GameObject.Destroy(GameObject.Find ("GameManager"));
        Application.LoadLevel("NameInput");
    }
}

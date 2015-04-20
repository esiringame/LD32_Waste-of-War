using UnityEngine;
using System.Collections;

public class StartCaseBehaviour : CaseBehaviour<StartCaseBehaviour> {

    public override bool IsObstacle
    {
        get { return false; }
    }

    public override void OnEnter(PlayerController player)
    {
        if (player.IsBucketFilled)
        {
            GameManager.Instance.DifferedChangeState(new VictoryGameState(GameManager.Instance));
        }
    }
}

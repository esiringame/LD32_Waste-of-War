using UnityEngine;
using System.Collections;

public class WellCaseBehaviour : CaseBehaviour<WellCaseBehaviour> {

    public override bool IsObstacle
    {
        get { return false; }
    }

    public override void OnEnter(PlayerController player)
    {
        if (!player.IsBucketFilled)
        {
            player.FillBucket();
        }
    }
}

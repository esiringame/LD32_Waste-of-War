using System;
using UnityEngine;
using System.Collections;

public class BorderCaseBehaviour : CaseBehaviour<BorderCaseBehaviour>
{
    public override bool IsObstacle
    {
        get { return true; }
    }

    public override void OnEnter(PlayerController player)
    {
        throw new NotImplementedException("YOU SHOULD NOT PASS");
    }

    public override void PutStone(PlayerController player)
    {
        throw new NotImplementedException("YOU SHOULD NOT PASS");
    }
}

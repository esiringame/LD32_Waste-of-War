using UnityEngine;
using System.Collections;
using System;

public class Obstacle : CaseBehaviour<Obstacle> {

    public override bool IsObstacle
    {
        get { return true; }
    }

    public override void onEnter(GameObject player)
    {
        throw new NotImplementedException("YOU SHOULD NOT PASS");
    }

    public override void putStone()
    {

    }
}

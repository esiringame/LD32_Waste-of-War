﻿using UnityEngine;
using System.Collections;
using System;

public class Obstacle : CaseBehaviour {

    public override bool isObstacle
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

using UnityEngine;
using System.Collections;

public class EmptyCaseBehaviour : CaseBehaviour<EmptyCaseBehaviour> {

    public override bool IsObstacle
    {
        get { return false; }
    }

    public override void OnEnter(PlayerController player)
    {
        if (HasStone && !player.IsInventoryFull())
        {
            player.AddRockToInventory();
            HasStone = false;
            RefreshObjectSprite();
        }
    }
}

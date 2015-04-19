using UnityEngine;
using System.Collections;

public class EmptyCaseBehaviour : CaseBehaviour<EmptyCaseBehaviour> {

    public override bool IsObstacle
    {
        get { return false; }
    }

    public override void onEnter(GameObject player)
    {
        if (HasStone)
        {
            //Ajouter un caillou au joueur (Verif capacité de l'inventaire)
            HasStone = false;
        }
    }
}

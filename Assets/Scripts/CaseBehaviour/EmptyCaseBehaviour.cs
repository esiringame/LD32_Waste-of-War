using UnityEngine;
using System.Collections;

public class EmptyCaseBehaviour : CaseBehaviour {

    public override bool isObstacle
    {
        get { return false; }
    }
    public override void onEnter(GameObject player)
    {
        if (hasStone)
        {
            //Ajouter un caillou au joueur (Verif capacité de l'inventaire)
            hasStone = false;
        }
    }
}

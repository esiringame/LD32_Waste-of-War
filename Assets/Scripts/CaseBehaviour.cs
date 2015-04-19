using UnityEngine;
using System.Collections;
using DesignPattern;

public abstract class CaseBehaviour : Factory<CaseBehaviour> {

    public int positionX;
    public int positionY;
    public bool hasStone = false;

    public abstract bool isObstacle { get; }

    abstract public void onEnter(GameObject player);
    virtual public void onLeave(GameObject player)
    {

    }

    virtual public void putStone()
    {
        if (hasStone)
        {
            //TU PEUX PAS ?! (Son, ou quelque chose...)
        }
        else
        {
            hasStone = true;
            //Mettre le Sprite du caillou
        }
    }

    public void setPosition(int x, int y)
    {
        positionX = x;
        positionY = y;
    }
}

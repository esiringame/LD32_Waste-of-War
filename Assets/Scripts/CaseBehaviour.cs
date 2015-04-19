using UnityEngine;
using System.Collections;
using DesignPattern;

public abstract class CaseBehaviour<T> : Factory<T>, ICaseBehaviour
    where T : Factory<T>
{
    public int PositionX { get; protected set; }
    public int PositionY { get; protected set; }
    public bool HasStone { get; protected set; }

    public abstract bool IsObstacle { get; }

    abstract public void onEnter(GameObject player);
    virtual public void onLeave(GameObject player)
    {

    }

    virtual public void putStone()
    {
        if (HasStone)
        {
            //TU PEUX PAS ?! (Son, ou quelque chose...)
        }
        else
        {
            HasStone = true;
            //Mettre le Sprite du caillou
        }
    }

    public void setPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }
}

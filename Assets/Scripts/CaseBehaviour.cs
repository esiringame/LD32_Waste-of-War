using UnityEngine;
using System.Collections;
using DesignPattern;

public abstract class CaseBehaviour<T> : Factory<T>, ICaseBehaviour
    where T : Factory<T>
{
    public GameObject GameObject
    {
        get { return gameObject; }
    }

    public int PositionX { get; protected set; }
    public int PositionY { get; protected set; }
    public bool HasStone { get; set; }

    public abstract bool IsObstacle { get; }

    abstract public void OnEnter(PlayerController player);
    virtual public void OnLeave(PlayerController player)
    {

    }

    virtual public void PutStone(PlayerController player)
    {
        if (HasStone)
        {
            //TU PEUX PAS ?! (Son, ou quelque chose...)
        }
        else
        {
            if (player.IsInventoryEmpty())
            {
                player.RemoveRockFromInventory();
                HasStone = true;
            }
            //Mettre le Sprite du caillou
        }
    }

    public void SetPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }

    public TBehaviour ChangeBehaviour<TBehaviour>()
        where TBehaviour : CaseBehaviour<TBehaviour>
    {
        TBehaviour newBehaviour = gameObject.AddComponent<TBehaviour>();
        Destroy(this);
        return newBehaviour;
    }
}

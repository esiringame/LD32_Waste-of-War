using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using DesignPattern;

public abstract class CaseBehaviour<T> : Factory<T>, ICaseBehaviour
    where T : Factory<T>
{
    public int PositionX { get; protected set; }
    public int PositionY { get; protected set; }
    public bool HasStone { get; set; }

    public GameObject Object;

    public abstract bool IsObstacle { get; }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = TilesetGallery.Instance.GetBackground();
        RefreshObjectSprite();
    }

    abstract public void OnEnter(PlayerController player);
    public virtual void OnLeave(PlayerController player) {}

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
        DestroyImmediate(this);

        RefreshObjectSprite();

        return newBehaviour;
    }

    void RefreshObjectSprite()
    {
        if (HasStone)
            Object.GetComponentInChildren<SpriteRenderer>().sprite = TilesetGallery.Instance.Stone;
        else
            Object.GetComponentInChildren<SpriteRenderer>().sprite = TilesetGallery.Instance.GetObject(GetType());
    }
}

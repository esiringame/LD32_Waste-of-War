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

    public Quaternion Rotation
    {
        set { transform.localRotation = value; }
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = TilesetGallery.Instance.GetBackground(GetType());
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
            HasStone = true;
            RefreshObjectSprite();
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
        newBehaviour.Object = Object;
        newBehaviour.SetPosition(PositionX, PositionY);
        newBehaviour.transform.parent = transform;
        newBehaviour.transform.position = new Vector3(PositionX + 0.5f, PositionY + 0.5f, 0);

        Destroy(GetComponent(GetType()));

        RefreshObjectSprite();

        return newBehaviour;
    }

    protected void RefreshObjectSprite()
    {
        if (HasStone)
            Object.GetComponentInChildren<SpriteRenderer>().sprite = TilesetGallery.Instance.Stone;
        else
            Object.GetComponentInChildren<SpriteRenderer>().sprite = TilesetGallery.Instance.GetObject(GetType());
    }
}

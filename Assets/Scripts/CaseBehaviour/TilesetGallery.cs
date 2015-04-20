using System;
using UnityEngine;
using DesignPattern;
using Random = UnityEngine.Random;

public class TilesetGallery : Singleton<TilesetGallery>
{
    public Sprite[] Sand;
    public Sprite[] Border;
    public Sprite Entry;
    public Sprite Stone;
    public Sprite Tree;
    public Sprite Well;
    public Sprite RedMine;
    public Sprite GreenMine;

    public Sprite GetBackground(Type type)
    {
        if (type == typeof(BorderCaseBehaviour))
            return Border[Random.Range(0, 3)];
        if (type == typeof(StartCaseBehaviour))
            return Entry;
        
        return Sand[Random.Range(0, 3)];
    }

    public Sprite GetObject(Type type)
    {
        if (type == typeof(Obstacle))
            return Tree;
        if (type == typeof(WellCaseBehaviour))
            return Well;
        if (type == typeof(RedMinesBehaviour))
            return RedMine;
        if (type == typeof(GreenMinesBehaviour))
            return GreenMine;

        return null;
    }
}

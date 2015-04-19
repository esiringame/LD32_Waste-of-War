using UnityEngine;
using System.Collections;
using DesignPattern;

public class Grid : DesignPattern.Singleton<Grid>
{
    public int Height = 10;
    public int Width = 30;
    public Vector2 StartCase;

    public ICaseBehaviour[][] grid { get; private set; }

    void Start()
    {
		grid = new ICaseBehaviour[Height][];
        for (int i = 0; i < grid.Length; i++)
            grid[i] = new ICaseBehaviour[Width];

        CaseData[][] dataGrid = MapGenerator.GenerateMap(Width, Height, 1);

        for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    CaseData currentCase = dataGrid[y][x];
                    ICaseBehaviour caseBehaviour;

                    switch (currentCase)
                    {
                        case CaseData.RedMines:
                            caseBehaviour = Factory<RedMinesBehaviour>.New("Case/RedMines");
                            break;
                        case CaseData.GreenMines:
                            caseBehaviour = Factory<GreenMinesBehaviour>.New("Case/GreenMines");
                            break;
                        case CaseData.Obstacle:
                            caseBehaviour = Factory<Obstacle>.New("Case/Obstacle");
                            break;
                        case CaseData.Stone:
                            caseBehaviour = Factory<EmptyCaseBehaviour>.New("Case/Stone");
                            caseBehaviour.HasStone = true;
                            break;
                        default:
                            caseBehaviour = Factory<EmptyCaseBehaviour>.New("Case/EmptyCase");
                            break;
                    }

                    caseBehaviour.SetPosition(x, y);
                    ((MonoBehaviour)caseBehaviour).transform.parent = transform;
                    ((MonoBehaviour)caseBehaviour).transform.position = new Vector3(x, y, 0);
                    grid[y][x] = caseBehaviour;
                }
            }
    }
}
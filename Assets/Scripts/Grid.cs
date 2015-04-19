using UnityEngine;
using System.Collections;
using DesignPattern;



public class Grid : DesignPattern.Singleton<Grid>
{
    public ICaseBehaviour[][] grid;
	
	public int Height
	{
		get { return grid.Length; }
	}
	public int Width
	{
		get { return grid [0].Length; }
	}

    public Vector2 StartCase
    {
        get { return new Vector2(1,1); }
    }

    void Start()
    {
        int h = 10;
        int w = 30;

		grid = new ICaseBehaviour[h][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new ICaseBehaviour[w];
        }

		CaseData[][] dataGrid = MapGenerator.GenerateMap(w, h, 1);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    CaseData currentCase = dataGrid[y][x];

                    switch (currentCase)
                    {
                        case CaseData.RedMines:
                            RedMinesBehaviour redMines = Factory<RedMinesBehaviour>.New("Case/RedMines");
                            redMines.SetPosition(x, y);
                            redMines.transform.position = new Vector3(x, y, 0);
                            grid[y][x] = redMines;
                            break;
                        case CaseData.GreenMines:
                            GreenMinesBehaviour greenMines = Factory<GreenMinesBehaviour>.New("Case/GreenMines");
                            greenMines.SetPosition(x, y);
                            greenMines.transform.position = new Vector3(x, y, 0);
                            grid[y][x] = greenMines;
                            break;
                        case CaseData.Obstacle:
                            Obstacle obstacle = Factory<Obstacle>.New("Case/Obstacle");
                            obstacle.SetPosition(x, y);
                            obstacle.transform.position = new Vector3(x, y, 0);
                            grid[y][x] = obstacle;
                            break;
                        case CaseData.Stone:
                            EmptyCaseBehaviour stoneCase = Factory<EmptyCaseBehaviour>.New("Case/Stone");
                            stoneCase.HasStone = true;
                            stoneCase.SetPosition(x, y);
                            stoneCase.transform.position = new Vector3(x, y, 0);
                            grid[y][x] = stoneCase;
                            break;
                        default:
                            EmptyCaseBehaviour emptyCase = Factory<EmptyCaseBehaviour>.New("Case/EmptyCase");
                            emptyCase.SetPosition(x, y);
                            emptyCase.transform.position = new Vector3(x, y, 0);
                            grid[y][x] = emptyCase;
                            break;
                    }
                }
            }
    }
}
using UnityEngine;
using System.Collections;
using DesignPattern;



public class Grid : MonoBehaviour
{

    public CaseData[][] dataGrid = new CaseData[5][];
    private CaseBehaviour[][] grid;
    
    void mapGenerator()
    {
        for (int i = 0; i < dataGrid.Length; i++)
        {
            dataGrid[i] = new CaseData[5];
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == 0)
                {
                    dataGrid[i][j] = CaseData.RedMines;
                }
                if (i == 1)
                {
                    dataGrid[i][j] = CaseData.GreenMines;
                }
                if (i == 2)
                {
                    dataGrid[i][j] = CaseData.Obstacle;
                }
                if (i == 3)
                {
                    dataGrid[i][j] = CaseData.EmptyCase;
                }
                if (i == 4)
                {
                    dataGrid[i][j] = CaseData.Stone;
                }
            }
        }
    }  
     
    void Start(){
        //mapGenerator();
        //int h = dataGrid.getLength(0);
        //int w = dataGrid.getLength(1);

        int h = 10;
        int w = 30;

        grid = new CaseBehaviour[h][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new CaseBehaviour[w];
        }

        dataGrid = MapGenerator.GenerateMap(w, h, 1);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    CaseData currentCase = dataGrid[y][x];

                    switch (currentCase)
                    {
                        case CaseData.RedMines:
                            grid[y][x] = Factory<CaseBehaviour>.New("Case/RedMines");
                            grid[y][x].transform.position = new Vector3(x, y, 0);
                            break;
                        case CaseData.GreenMines:
                            grid[y][x] = Factory<CaseBehaviour>.New("Case/GreenMines");
                            grid[y][x].transform.position = new Vector3(x, y, 0);
                            break;
                        case CaseData.Obstacle:
                            grid[y][x] = Factory<CaseBehaviour>.New("Case/Obstacle");
                            grid[y][x].transform.position = new Vector3(x, y, 0);
                            break;
                        case CaseData.Stone:
                            grid[y][x] = Factory<CaseBehaviour>.New("Case/Stone");
                            grid[y][x].transform.position = new Vector3(x, y, 0);
                            break;
                        default:
                            grid[y][x] = Factory<CaseBehaviour>.New("Case/EmptyCase");
                            grid[y][x].transform.position = new Vector3(x, y, 0);
                            break;
                    }
                }
            }
    }
}
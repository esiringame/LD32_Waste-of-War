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
        mapGenerator();
        //int h = dataGrid.getLength(0);
        //int w = dataGrid.getLength(1);
        int h = 5;
        int w = 5;

        grid = new CaseBehaviour[h][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new CaseBehaviour[w];
        }


            for (int x = 0; x < h; x++)
            {
                for (int y = 0; y < w; y++)
                {
                    CaseData currentCase = dataGrid[x][y];

                    switch (currentCase)
                    {
                        case CaseData.RedMines:
                            grid[x][y] = Factory<CaseBehaviour>.New("Case/RedMines");
                            grid[x][y].transform.position = new Vector3(x, y, 0);
                            break;
                        case CaseData.GreenMines:
                            grid[x][y] = Factory<CaseBehaviour>.New("Case/GreenMines");
                            grid[x][y].transform.position = new Vector3(x, y, 0);
                            break;
                        case CaseData.Obstacle:
                            grid[x][y] = Factory<CaseBehaviour>.New("Case/Obstacle");
                            grid[x][y].transform.position = new Vector3(x, y, 0);
                            break;
                        case CaseData.Stone:
                            grid[x][y] = Factory<CaseBehaviour>.New("Case/Stone");
                            grid[x][y].transform.position = new Vector3(x, y, 0);
                            break;
                        default:
                            grid[x][y] = Factory<CaseBehaviour>.New("Case/EmptyCase");
                            grid[x][y].transform.position = new Vector3(x, y, 0);
                            break;
                    }
                }
            }
    }
}
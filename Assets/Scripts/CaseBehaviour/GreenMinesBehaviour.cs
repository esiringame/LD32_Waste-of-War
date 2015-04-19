using UnityEngine;
using System.Collections.Generic;
using DesignPattern;

public class GreenMinesBehaviour : CaseBehaviour {

    public AudioClip boom;
    public AudioClip mineArmed;

    public override bool isObstacle
    {
        get { return false; }
    }

    public override void onEnter(GameObject player)
    {
        GetComponent<AudioSource>().PlayOneShot(mineArmed, 1.0F);
        //player.mode = mode.GreenMine;
    }

    public override void onLeave(GameObject player)
    {
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
        fragmentation();
        //player.die
    }

    public override void putStone()
    {
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
        fragmentation();
    }

    //Genère les cases adjacentes à la mine verte
    private List<CaseBehaviour> casesAdjacentes()
    {
        List<CaseBehaviour> adjacents = new System.Collections.Generic.List<CaseBehaviour>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                // Condition aux bords
                if (positionX + i < 10 && positionY + j < 30 && positionX + i > 0 && positionY + j > 0)
                {
                    CaseBehaviour CurrentCase = Grid.Instance.grid[j][i];
                    // On ne prend pas en compte les mines et obstacles
                    if (CurrentCase is EmptyCaseBehaviour)
                    {
                        adjacents.Add(CurrentCase);
                    }
                }
            }
        }
        return adjacents;     
    }

    public void fragmentation()
    {
        List<CaseBehaviour> adjacents = casesAdjacentes();
   
        for (int i = 0; i < 3 && adjacents.Count >0; i++)
        {
            // Choisit aléatoirement une des cases adjacentes
            
            CaseBehaviour randomCase = adjacents[Random.Range(0,adjacents.Count-1)];
            int x= randomCase.positionX;
            int y= randomCase.positionY;
            // On la supprime de la liste pour ne pas retomber dessus
            adjacents.Remove(randomCase);

            // Fragmentation d'une mine verte ou range
            if (Random.value > 0.5f)
            {
                Grid.Instance.grid[y][x] = Factory<CaseBehaviour>.New("Case/RedMines");
            }

            else
            {
                Grid.Instance.grid[y][x] = Factory<CaseBehaviour>.New("Case/GreenMines");
            }
        }
        // Transforme cette case en case vide
        Grid.Instance.grid[positionY][positionX] = Factory<CaseBehaviour>.New("Case/EmptyCase"); 
    }
}

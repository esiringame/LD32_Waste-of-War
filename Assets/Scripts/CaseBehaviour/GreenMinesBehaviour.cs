using UnityEngine;
using System.Collections.Generic;
using DesignPattern;

public class GreenMinesBehaviour : CaseBehaviour<GreenMinesBehaviour>
{
    public AudioClip boom;
    public AudioClip mineArmed;
    private float timer;
    PlayerController m_player = null;
	private float timerVisible = 10;

    void Update()
	{
		if (HasStone)
		{
			Object.GetComponent<SpriteRenderer>().enabled = true;
		}
		else if(timerVisible <= 3)
		{
			Object.GetComponent<SpriteRenderer>().enabled = true;
			timerVisible += Time.unscaledDeltaTime;
		}
		else
		{
			Object.GetComponent<SpriteRenderer>().enabled = false;
		}

		if (m_player != null)
        {
            timer += Time.unscaledDeltaTime;
        }
        if (timer > 2.0f)
        {
            Fragmentation();
            if (m_player != null)
            {
				setMineVisible(true);
				m_player.Die();
            }
        }
    }

    public override bool IsObstacle
    {
        get { return false; }
    }

    public override void OnEnter(PlayerController player)
    {
        GetComponent<AudioSource>().PlayOneShot(mineArmed, 1.0F);
        timer = 0.0f;
        m_player = player;
    }

    public override void OnLeave(PlayerController player)
    {
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
        Fragmentation();
        m_player = null;

        if (!player.IsJumping)
		{
			setMineVisible(true);
			player.Die ();
		}
    }

    public override void PutStone(PlayerController player)
    {
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
		m_player = player;
        Fragmentation();
		m_player = null;
    }

    public void Fragmentation()
    {
		List<ICaseBehaviour> adjacents = CasesAdjacentes();
   
        for (int i = 0; i < 3 && adjacents.Count > 0; i++)
        {
            // Choisit aléatoirement une des cases adjacentes
            
            ICaseBehaviour randomCase = adjacents[Random.Range(0,adjacents.Count)];
            int x = randomCase.PositionX;
            int y = randomCase.PositionY;
            // On la supprime de la liste pour ne pas retomber dessus
            adjacents.Remove(randomCase);

            // Fragmentation d'une mine verte ou rouge
            if (Random.value > 0.5f)
            {
                Grid.Instance.grid[y][x] = Grid.Instance.grid[y][x].ChangeBehaviour<RedMinesBehaviour>();
				(Grid.Instance.grid[y][x] as RedMinesBehaviour).setMineVisible(true);
            }
            else
            {
				Grid.Instance.grid[y][x] = Grid.Instance.grid[y][x].ChangeBehaviour<GreenMinesBehaviour>();
				(Grid.Instance.grid[y][x] as GreenMinesBehaviour).setMineVisible(true);
			}
		}
        // Transforme cette case en case vide
        Grid.Instance.grid[PositionY][PositionX] = Grid.Instance.grid[PositionY][PositionX].ChangeBehaviour<EmptyCaseBehaviour>();
		(Grid.Instance.grid [PositionY] [PositionX] as EmptyCaseBehaviour).AddRemanantMine (TilesetGallery.Instance.GreenMine);
    }

    //Genère les cases adjacentes à la mine verte
    private List<ICaseBehaviour> CasesAdjacentes()
    {
        List<ICaseBehaviour> adjacents = new List<ICaseBehaviour>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                // Condition aux bords
                if (PositionY + i < Grid.Instance.Height && PositionX + j < Grid.Instance.Width && PositionY + i >= 0 && PositionX + j >= 0)
                {
					if(((PositionY + i + 0.5f) != m_player.PositionCase.y) && ((PositionX + j + 0.5f) != m_player.PositionCase.x))
					{
						ICaseBehaviour CurrentCase = Grid.Instance.grid[PositionY+i][PositionX+j];
						// On ne prend pas en compte les mines et obstacles
						if (CurrentCase is EmptyCaseBehaviour && !CurrentCase.HasStone)
						{
							adjacents.Add(CurrentCase);
						}
					}
                }
            }
        }
        return adjacents;
	}
	
	public void setMineVisible(bool visible)
	{
		timerVisible = visible ? 0 : 10;
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (transform.position, 0.5f);
	}
}

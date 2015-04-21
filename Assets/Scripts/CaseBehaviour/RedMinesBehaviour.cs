using UnityEngine;
using System.Collections;

public class RedMinesBehaviour : CaseBehaviour<RedMinesBehaviour> {

    public AudioClip mineArmed;
    public AudioClip mineDisarmed;
	public AudioClip boom;
	private float timerVisible = 0;
	
    public override bool IsObstacle
    {
        get { return false; }
    }

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
	}

    public override void OnEnter(PlayerController player)
    {
        if (!HasStone)
        {
            GetComponent<AudioSource>().PlayOneShot(mineArmed, 1.0F);
        }
        else
        {
            if (!player.IsInventoryFull())
            {
                Explosion(player);
            }
        }
    }

    public override void OnLeave(PlayerController player)
    {
        if (!HasStone)
        {
            Explosion(player);
        }
    }

    public override void PutStone(PlayerController player)
    {
        if (!HasStone)
        {
			base.PutStone(player);
            GetComponent<AudioSource>().PlayOneShot(mineDisarmed, 1.0F);
        }
    }

    private void Explosion(PlayerController player)
    {
        player.Die();
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
		Grid.Instance.grid[PositionY][PositionX] = Grid.Instance.grid[PositionY][PositionX].ChangeBehaviour<EmptyCaseBehaviour>();
		(Grid.Instance.grid [PositionY] [PositionX] as EmptyCaseBehaviour).AddRemanantMine (TilesetGallery.Instance.RedMine);
	}
	
	public void setMineVisible(bool visible)
	{
		timerVisible = visible ? 0 : 10;
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (transform.position, 0.5f);
	}
}

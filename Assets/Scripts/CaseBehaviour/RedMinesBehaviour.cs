using UnityEngine;
using System.Collections;

public class RedMinesBehaviour : CaseBehaviour<RedMinesBehaviour> {

    public AudioClip mineArmed;
    public AudioClip mineDisarmed;
    public AudioClip boom;

    public override bool IsObstacle
    {
        get { return false; }
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
            GetComponent<AudioSource>().PlayOneShot(mineDisarmed, 1.0F);
        }
    }

    private void Explosion(PlayerController player)
    {
        player.Die();
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
        Grid.Instance.grid[PositionY][PositionX] = Grid.Instance.grid[PositionY][PositionX].ChangeBehaviour<EmptyCaseBehaviour>();
    }
}

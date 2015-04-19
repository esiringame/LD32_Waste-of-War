using UnityEngine;
using System.Collections;

public class RedMinesBehaviour : CaseBehaviour {

    public AudioClip mineArmed;
    public AudioClip mineDisarmed;
    public AudioClip boom;

    private bool isArmed = true;

    public override bool isObstacle
    {
        get { return false; }
    }

    public override void onEnter(GameObject player)
    {
        if (isArmed)
        {
            GetComponent<AudioSource>().PlayOneShot(mineArmed, 1.0F);
        }
        else
        {
            if (false)//!player.bagIsFull())
            {
                explosion(player);
            }
        }
    }

    public override void onLeave(GameObject player)
    {
        if (isArmed)
        {
            explosion(player);
        }
    }

    public override void putStone()
    {
        if (isArmed)
        {
            isArmed = false;
            GetComponent<AudioSource>().PlayOneShot(mineDisarmed, 1.0F);
        }
    }

    private void explosion(GameObject player)
    {
        //player.die;
        GetComponent<AudioSource>().PlayOneShot(boom, 1.0F);
    }
}

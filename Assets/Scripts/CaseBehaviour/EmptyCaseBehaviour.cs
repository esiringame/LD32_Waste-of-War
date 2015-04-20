using UnityEngine;
using System.Collections;

public class EmptyCaseBehaviour : CaseBehaviour<EmptyCaseBehaviour>
{
	private float visible = 10;
	private bool remanant = false;

    public override bool IsObstacle
    {
        get { return false; }
    }

    public override void OnEnter(PlayerController player)
    {
        if (HasStone && !player.IsInventoryFull())
        {
            player.AddRockToInventory();
            HasStone = false;
            RefreshObjectSprite();
        }
    }

	void Update()
	{
		if (remanant && visible > 3)
		{
			remanant = false;
			RefreshObjectSprite ();
		}
		else if (remanant)
			visible += Time.deltaTime;
	}

	public void AddRemanantMine(Sprite sprite)
	{
		visible = 0;
		remanant = true;
		Object.GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	protected override void RefreshObjectSprite ()
	{
		if (!remanant)
			base.RefreshObjectSprite ();
	}
}

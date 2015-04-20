using UnityEngine;
using System.Collections.Generic;
using DesignPattern;

public class StoneTrajectory : DesignPattern.Factory<StoneTrajectory>
{
	public PlayerController player;
	public Vector2 posDeparture;
	public Vector2 posArrival;

	private Vector3 pos3DDeparture;
	private Vector3 pos3DArrival;
	private Vector3 point3DUn;
	private Vector3 point3DDeux;

	private float time = 0;
	private float speed = 1f;

	void Update()
	{
		pos3DDeparture.x = (float) (posDeparture.x + 0.5);
		pos3DDeparture.y = (float) (posDeparture.y + 0.5);
		pos3DDeparture.z = - 5;
		pos3DArrival.x = (float) (posArrival.x + 0.5);
		pos3DArrival.y = (float) (posArrival.y + 0.5);
		pos3DArrival.z = - 5;
		point3DUn = pos3DDeparture;
		point3DUn.y += 2;
		point3DDeux = pos3DArrival;
		point3DDeux.y += 2;

		time += speed * Time.deltaTime;
		time = Mathf.Clamp01(time);
		this.transform.position = GetPoint(pos3DDeparture, point3DUn, point3DDeux, pos3DArrival, time);

		if (time >= 1)
		{
			Destroy(this.gameObject);
			Grid.Instance.grid[(int)posArrival.y][(int)posArrival.x].PutStone(player);
		}

	}

	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		t = Mathf.Clamp01(t);
		float oneMinusT = 1f - t;
		return
			oneMinusT * oneMinusT * oneMinusT * p0 +
				3f * oneMinusT * oneMinusT * t * p1 +
				3f * oneMinusT * t * t * p2 +
				t * t * t * p3;
	}


	 
}
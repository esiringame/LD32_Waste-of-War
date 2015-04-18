using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	public float PosX=1;
	public float PosY=1;
	public float PosZ=1;


	void Start () {
		PosX = transform.position.x;
		PosY = transform.position.y;
		PosZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		PosX = transform.position.x;
		PosY = transform.position.y;
		PosZ = transform.position.z;
		/*if (PosX < 5) 
			transform.position = transform.position + new Vector3 (0.1f, 0, 0);
		else
			transform.position = transform.position + new Vector3 (-0.3f, 0, -0.2f);
		if (PosZ < 5)
			transform.position = transform.position + new Vector3 (0, 0, 0.1f);
		else
			transform.position = transform.position + new Vector3 (-0.2f, 0, -0.3f);*/
		GetComponent<Rigidbody> ().AddTorque (new Vector3 (1, 1, 1));
	}
}

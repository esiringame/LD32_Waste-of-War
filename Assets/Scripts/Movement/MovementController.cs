using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	public float PosX=1;
	public float PosY=1;
	public float PosZ=1;
	private Vector3 east = Vector3.right;
	private Vector3 north = Vector3.forward;
	private Vector3 west = Vector3.left;
	private Vector3 south = Vector3.back;
	public Vector3 Direction;
	public float TimeKeyPressed;
	private float startTime;
	public float NextCaseDistance = 0.1f;
	private Vector3 startMarker;
	private Vector3 endMarker;


	void Start () {
		PosX = transform.position.x;
		PosY = transform.position.y;
		PosZ = transform.position.z;
		Direction = Vector3.zero;
		TimeKeyPressed = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");
		PosX = transform.position.x;
		PosY = transform.position.y;
		PosZ = transform.position.z; 
		/*switch(3*horizontalInput + verticalInput) {
		case 5 :
		case 3:
		case 2:
		case 1:
		case 0:
		case -1:
		case -2 :
		case -3:
		case -5:
		default :
		}*/
		//if (TimeKeyPressed + 0.5f < Time.realtimeSinceStartup) {
			if (horizontalInput > 0)
				Direction = east;
			else if (horizontalInput < 0)
				Direction = west;
			else if (verticalInput > 0)
				Direction = north;
			else if (verticalInput < 0)
				Direction = south;
			else
				return;
			if (Direction != transform.forward) {
				transform.forward = Direction;
			} else {
				startTime = Time.time;
				startMarker = transform.position;
				endMarker = startMarker+NextCaseDistance*transform.forward;
				//while(Time.time<0.5f)
					GoForward ();
			}
			TimeKeyPressed = Time.realtimeSinceStartup;
		//}


		/*if(horizontalInput!=0)
		transform.forward=horizontalInput*transform.right;*/
	}

	void GoForward() {
		transform.position = transform.position + NextCaseDistance * transform.forward;
		/*float fracJourney = (Time.time-startTime)/0.5f;
		transform.position=Vector3.Lerp(startMarker,endMarker,fracJourney);*/

	}
}
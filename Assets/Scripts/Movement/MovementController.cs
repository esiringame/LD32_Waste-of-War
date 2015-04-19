using UnityEngine;
using System.Collections;


public class MovementController : MonoBehaviour
{
	public Vector2 PositionCase { get; private set; }
	public float Speed = 1;

	private static Vector3 east = Vector3.right;
	private static Vector3 north = Vector3.up;
	private static Vector3 west = Vector3.left;
	private static Vector3 south = Vector3.down;
	private const float CaseSize = 1;

	public Vector3 Direction { get; private set; }
	public Vector3 Destination { get; private set; }

	private float pressedTimeElapsed;
	private const float PressedTimePeriod = 0.2f;

	void Start ()
	{
		Direction = Vector3.right;
		Destination = transform.position;

		pressedTimeElapsed = 0;
	}

	void Update ()
	{
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		if (transform.position == Destination)
		{
			Vector3 newDirection = Vector3.zero;

			if (horizontalInput > 0)
				newDirection = east;
			else if (horizontalInput < 0)
				newDirection = west;
			else if (verticalInput > 0)
				newDirection = north;
			else if (verticalInput < 0)
				newDirection = south;

			if (newDirection == Vector3.zero)
				pressedTimeElapsed = 0;
			if (Direction == newDirection)
			{
				pressedTimeElapsed += Time.deltaTime;
				
				if (pressedTimeElapsed >= PressedTimePeriod)
					Destination += Direction * CaseSize;
			}
			else
				pressedTimeElapsed = 0;
			
			if (newDirection != Vector3.zero)
				Direction = newDirection;
		}

		if (transform.position != Destination)
		{
			if (Vector3.Dot(Direction, Destination - this.transform.position) > 0)
				transform.position += Direction * Speed * Time.deltaTime;
			else
				transform.position = Destination;
		}
	}
}
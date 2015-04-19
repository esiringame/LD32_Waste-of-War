using UnityEngine;
using System.Collections;

public class PlayerControlToSuppress : MonoBehaviour {
	
	public float walkSpeed = 1; // player left right walk speed
	private bool _isGrounded = true; // is player on the ground?
	
	Animator animator;
	
	//animation states - the values in the animator conditions
	const int STATE_IDLE = 0;
	const int STATE_WALK = 1;
	
	string _currentDirection = "left";
	int _currentAnimationState = STATE_IDLE;
	
	// Use this for initialization
	void Start()
	{
		//define the animator attached to the player
		animator = this.GetComponent<Animator>();
	
	}
	
	// FixedUpdate is used insead of Update to better handle the physics based jump
	void FixedUpdate()
	{
		
		if (Input.GetKey ("left") )
		{
			changeDirection ("right");
			transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
			
			if(_isGrounded)
				changeState(STATE_WALK);

		}
		else if (Input.GetKey ("right") )
		{
			changeDirection ("left");
			transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
			
			if(_isGrounded)
				changeState(STATE_WALK);

			
		}
		else
		{
			if(_currentDirection=="right" || _currentDirection=="left")
				changeState(STATE_IDLE);
		}
		
	}
	
	//--------------------------------------
	// Change the players animation state
	//--------------------------------------
	void changeState(int state){
		
		if (_currentAnimationState == state)
			return;
		
		switch (state) {
			
		case STATE_WALK:
			animator.SetInteger ("state", STATE_WALK);
			break;
			
		case STATE_IDLE:
			animator.SetInteger ("state", STATE_IDLE);
			break;
			
		}

		_currentAnimationState = state;
	}
	
	//--------------------------------------
	// Check if player has collided with the floor
	//--------------------------------------
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Floor")
		{
			_isGrounded = true;
			changeState(STATE_IDLE);
			
		}
		
	}
	
	//--------------------------------------
	// Flip player sprite for left/right walking
	//--------------------------------------
	void changeDirection(string direction)
	{
		
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				transform.Rotate (0, 180, 0);
				_currentDirection = "right";
			}
			else if (direction == "left")
			{
				transform.Rotate (0, 180, 0);
				_currentDirection = "left";
			}
		}
		
	}
	
}
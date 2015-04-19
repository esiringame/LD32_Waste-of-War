using UnityEngine;
using System.Collections;

public class PlayerControlToSuppress : MonoBehaviour {
	
	public float walkSpeed = 1; // player left right walk speed
	
	Animator animator;
	//animation states - the values in the animator conditions
	const int STATE_IDLE_R = 0;
	const int STATE_WALK_R = 10;
	const int STATE_IDLE_L = 1;
	const int STATE_WALK_L = 11;
	const int STATE_IDLE_B = 3;
	const int STATE_WALK_B = 13;
	const int STATE_IDLE_T = 2;
	const int STATE_WALK_T = 12;
	
	string currentDirection = "right";
	int _currentAnimationState = STATE_IDLE_L;
	
	// Use this for initialization
	void Start()
	{
		//define the animator attached to the player
		animator = this.GetComponent<Animator>();
		}
	
	// FixedUpdate is used insead of Update to better handle the physics based jump
	void FixedUpdate()
	{
		
		if (Input.GetKey ("right"))
		{
			if(currentDirection =="right"){
				transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
				changeState(STATE_WALK_R);
			}else{
				changeState(STATE_IDLE_R);
				this.currentDirection="right";
			}
		}
		else if (Input.GetKey ("left") )
		{
			if(currentDirection=="left"){
				transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
				
				changeState(STATE_WALK_L);
			}else{
				changeState(STATE_IDLE_L);
				
				this.currentDirection="left";
				
			}
			
		}else if (Input.GetKey ("up") )
		{
			if(currentDirection=="top"){
				
				transform.Translate(Vector3.left * 0 * Time.deltaTime);
				changeState(STATE_WALK_T);
			}else{
				changeState(STATE_IDLE_T);
				
				this.currentDirection="top";
				
			}
			
		}else if (Input.GetKey ("down") )
		{
			if(currentDirection=="back"){
				transform.Translate(Vector3.left * 0 * Time.deltaTime);
				changeState(STATE_WALK_B);
			}else{
				changeState(STATE_IDLE_B);
				
				this.currentDirection="back";
				
			}
			
		}
		else
		{
			if(currentDirection=="right"){
				changeState(STATE_IDLE_R);
			}else if(currentDirection=="left"){
				changeState (STATE_IDLE_L);
			}
			else if(currentDirection=="top"){
				changeState (STATE_IDLE_T);
			}else if(currentDirection=="back"){
				changeState (STATE_IDLE_B);
			}
		}
		
	}
	
	//--------------------------------------
	// Change the players animation state
	//--------------------------------------
	void changeState(int state){
		
		if (_currentAnimationState == state)
			return;
		
		switch (state) {
			
		case STATE_WALK_R:
			animator.SetInteger ("state", STATE_WALK_R);
			break;
			
		case STATE_IDLE_R:
			animator.SetInteger ("state", STATE_IDLE_R);
			break;
			
		case STATE_WALK_L:
			animator.SetInteger ("state", STATE_WALK_L);
			break;
			
		case STATE_IDLE_L:
			animator.SetInteger ("state", STATE_IDLE_L);
			break;

		case STATE_WALK_T:
			animator.SetInteger ("state", STATE_WALK_T);
			break;
			
		case STATE_IDLE_T:
			animator.SetInteger ("state", STATE_IDLE_T);
			break;

		case STATE_WALK_B:
			animator.SetInteger ("state", STATE_WALK_B);
			break;
			
		case STATE_IDLE_B:
			animator.SetInteger ("state", STATE_IDLE_B);
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
			changeState(STATE_IDLE_R);
			
		}
		
	}
	
}
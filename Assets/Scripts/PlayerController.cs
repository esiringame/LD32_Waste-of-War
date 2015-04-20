using System;
using UnityEngine;
using System.Collections;
using DesignPattern;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public int LifesAtStartup = 5;
    public int RocksAtStartup = 1;
    public bool IsBucketFilled = false;

    public bool ControlEnabled = true;
    public float MoveSpeed = 3;

    public int Lifes;
    public int Rocks;
    public Vector2 PositionCase { get; private set; }
    public bool IsJumping { get; private set; }

    public const int MaxStones = 3;

    private const float CaseSize = 1;
    private static readonly Vector3 East = Vector3.right;
    private static readonly Vector3 North = Vector3.up;
    private static readonly Vector3 West = Vector3.left;
    private static readonly Vector3 South = Vector3.down;

    public Vector3 Direction { get; private set; }
    public Vector3 Destination { get; private set; }

    private float pressedTimeElapsed;
    private const float PressedTimePeriod = 0.2f;

    private bool alreadyLeaveCase;

	Animator animator;
	//animation states - the values in the animator conditions
	const int STATE_IDLE_R = 0;
	const int STATE_WALK_R = 10;
	const int STATE_JUMP_R = 30;
	const int STATE_THROW_R = 40;
	const int STATE_IDLE_L = 1;
	const int STATE_WALK_L = 11;
	const int STATE_JUMP_L = 31;
	const int STATE_THROW_L = 41;
	const int STATE_IDLE_B = 3;
	const int STATE_WALK_B = 13;
	const int STATE_JUMP_B = 33;
	const int STATE_THROW_B = 43;
	const int STATE_IDLE_T = 2;
	const int STATE_WALK_T = 12;
	const int STATE_JUMP_T = 32;
	const int STATE_THROW_T = 42;
	const int STATE_DIE = 20;

	string currentDirection = "right";
	int _currentAnimationState = STATE_IDLE_L;

    public ICaseBehaviour CurrentCase
    {
        get { return Grid.Instance.grid[(int)PositionCase.y][(int)PositionCase.x]; }
    }

    public bool IsMoving
    {
        get { return transform.position != Destination; }
    }

    public AudioClip dead, trash_dead, water;

    void Start()
	{
		//define the animator attached to the player
		animator = this.GetComponent<Animator>();
        Lifes = LifesAtStartup;
        Reset();
    }

    void Update()
    {
		HandleInput();

		if(IsMoving && !IsJumping){
			if (Direction == East) {
				
				changeState (STATE_WALK_R);
				
			} else if (Direction == West ) {
				
				changeState (STATE_WALK_L);
				
			} else if (Direction == North) {
				
				changeState (STATE_WALK_T );
				
			} else if (Direction == South) {
				
				changeState (STATE_WALK_B);
				
			}
		}else if(IsJumping){
			if (Direction == East) {
				
				changeState (STATE_JUMP_R);
				
			} else if (Direction == West ) {
				
				changeState (STATE_JUMP_L);
				
			} else if (Direction == North) {
				
				changeState (STATE_JUMP_T );
				
			} else if (Direction == South) {
				
				changeState (STATE_JUMP_B);
				
			}
		}else if(!IsJumping && !IsMoving){
			if (Direction == East) {
				
				changeState (STATE_IDLE_R);
				
			} else if (Direction == West ) {
				
				changeState (STATE_IDLE_L);
				
			} else if (Direction == North) {
				
				changeState (STATE_IDLE_T );
				
			} else if (Direction == South) {
				
				changeState (STATE_IDLE_B);
				
			}
		}
        if (transform.position != Destination) {
			if (Vector3.Dot (Direction, Destination - transform.position) > 0) {
				transform.position += Direction * MoveSpeed * Time.deltaTime;


				Vector3 lastPosition = new Vector3 (PositionCase.x * CaseSize, PositionCase.y * CaseSize, transform.position.z);


				if (!alreadyLeaveCase && (Destination - transform.position).magnitude < 2 * (Destination - lastPosition).magnitude / 3) {
					CurrentCase.OnLeave (this);
					alreadyLeaveCase = true;


				}
				
			} else {

				transform.position = Destination;
                PositionCase += new Vector2(Direction.x, Direction.y) * (IsJumping ? 2 : 1);
				CurrentCase.OnEnter (this);

				alreadyLeaveCase = false;
			    IsJumping = false;
			}
		} else
		{
			if(Direction == East){
				changeState(STATE_IDLE_R);
			}else if(Direction == West){
				changeState(STATE_IDLE_L);
			}else if(Direction == North){
				changeState(STATE_IDLE_T);
			}else if(Direction == South){
				changeState(STATE_IDLE_B);
			}
		}
    }

    void HandleInput()
    {
        if (!ControlEnabled)
            return;

        if (!alreadyLeaveCase && transform.position == Destination)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 newDirection = Vector3.zero;

            if (horizontalInput > 0)
                newDirection = East;
            else if (horizontalInput < 0)
                newDirection = West;
            else if (verticalInput > 0)
                newDirection = North;
            else if (verticalInput < 0)
                newDirection = South;

            if (newDirection == Vector3.zero)
                pressedTimeElapsed = 0;
            if (Direction == newDirection)
            {
                pressedTimeElapsed += Time.deltaTime;

                if (pressedTimeElapsed >= PressedTimePeriod)
                {
                    if (!CheckObstacle(newDirection))
                    {
                        Destination += Direction * CaseSize;
                    }
                }
            }
            else
                pressedTimeElapsed = 0;

            if (newDirection != Vector3.zero)
                Direction = newDirection;

            if (!IsMoving && Input.GetKeyDown(KeyCode.Space))
                Jump();
			

        }
    }

    public void Reset()
    {
        PositionCase = Grid.Instance.StartCase + Vector2.one * 0.5f;
        transform.position = new Vector3(PositionCase.x * CaseSize, PositionCase.y * CaseSize, this.transform.position.z);

        Direction = Vector3.right;
        Destination = transform.position;
        pressedTimeElapsed = 0;

        Rocks = 0;
        IsBucketFilled = false;

        alreadyLeaveCase = false;
    }

    public void Die()
    {
        --Lifes;
        GetComponent<AudioSource>().PlayOneShot(Random.value > 0.5 ? dead : trash_dead, 1.0f);
		changeState (STATE_DIE);
        if (IsGameOver())
            GameManager.Instance.ChangeState(new GameOverState(GameManager.Instance));
        else
            GameManager.Instance.ChangeState(new DeathGameState(GameManager.Instance));
    }

    public void Jump()
    {
        Vector3 jump = 2 * Direction;

        if (!CheckObstacle(jump) && !CheckObstacle(Direction))
        {
            Destination += jump * CaseSize;
            IsJumping = true;
        }
    }
    public void PutStone()
    {
        ICaseBehaviour caseBehaviour = Grid.Instance.grid[(int)PositionCase.y][(int)PositionCase.x];
        if (!IsInventoryEmpty () && !caseBehaviour.HasStone)
		{
			Rocks--;
			caseBehaviour.PutStone (this);
		}
    }

    public void ThrowStone(int x, int y)
    {
		ICaseBehaviour caseBehaviour = Grid.Instance.grid[y][x];
		if (IsInventoryEmpty () || caseBehaviour.HasStone)
			return;

		Rocks--;

		//Defining posArrival
		Vector2 posArrival;
		posArrival.x = x;
		posArrival.y = y;

		StoneTrajectory trajectory = Factory<StoneTrajectory>.New("Stone/StoneTrajectory");
		trajectory.player = this;
		trajectory.posDeparture = this.PositionCase;
		trajectory.posArrival = posArrival;

		if (Direction == East) {
			
			changeState (STATE_THROW_R);
			
		} else if (Direction == West ) {
			
			changeState (STATE_THROW_L);
			
		} else if (Direction == North) {
			
			changeState (STATE_THROW_T );
			
		} else if (Direction == South) {
			
			changeState (STATE_THROW_B);
			
		}
    }

    bool CheckObstacle(Vector2 newDirection)
    {
        Vector2 misDirection = new Vector2(PositionCase.x + newDirection.x, PositionCase.y + newDirection.y);
        if (misDirection.y >= 0 && misDirection.x >= 0 && misDirection.x < Grid.Instance.Width
            && misDirection.y < Grid.Instance.Height)
        {
            if (Grid.Instance.grid[(int)misDirection.y][(int)misDirection.x].IsObstacle)
                return true;
        }
        else
            return true;

        return false;
    }

    public bool IsGameOver()
    {
        return Lifes <= 0;
    }

    public bool IsInventoryFull()
    {
        return Rocks >= MaxStones;
    }

    public bool IsInventoryEmpty()
    {
        return Rocks <= 0;
    }

    public void FillBucket()
    {
        GetComponent<AudioSource>().PlayOneShot(water, 1.0f);
        IsBucketFilled = true;
    }

    public void EmptyBucket()
    {
        IsBucketFilled = false;
    }

    public void AddRockToInventory()
    {
        if (IsInventoryFull())
            throw new InvalidOperationException("Can't add a stone to full inventory ! Check inventory status before.");

        ++Rocks;
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
			
		case STATE_JUMP_B:
			animator.SetInteger ("state", STATE_JUMP_B);
			break;
			
		case STATE_JUMP_T:
			animator.SetInteger ("state", STATE_JUMP_T);
			break;
			
		case STATE_JUMP_R:
			animator.SetInteger ("state", STATE_JUMP_R);
			break;
			
		case STATE_JUMP_L:
			animator.SetInteger ("state", STATE_JUMP_L);
			break;

		case STATE_THROW_B:
			animator.SetInteger ("state", STATE_THROW_B);
			break;
			
		case STATE_THROW_T:
			animator.SetInteger ("state", STATE_THROW_T);
			break;
			
		case STATE_THROW_R:
			animator.SetInteger ("state", STATE_THROW_R);
			break;
			
		case STATE_THROW_L:
			animator.SetInteger ("state", STATE_THROW_L);
			break;
			
		case STATE_DIE:
			animator.SetInteger ("state", STATE_DIE);
			break;
		}
		
		_currentAnimationState = state;
	}
}

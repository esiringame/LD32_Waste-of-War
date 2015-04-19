using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public int LifesAtStartup = 5;
    public int RocksAtStartup = 1;
    public bool IsBucketFilled = false;
    public float MoveSpeed = 1;

    public int Lifes { get; private set; }
    public int Rocks { get; private set; }
    public Vector2 PositionCase { get; private set; }

    private const float CaseSize = 1;
    private static readonly Vector3 East = Vector3.right;
    private static readonly Vector3 North = Vector3.up;
    private static readonly Vector3 West = Vector3.left;
    private static readonly Vector3 South = Vector3.down;

    public Vector3 Direction { get; private set; }
    public Vector3 Destination { get; private set; }

    private float pressedTimeElapsed;
    private const float PressedTimePeriod = 0.2f;

    public ICaseBehaviour currentCase { get; private set; }
    public AudioClip dead, trash_dead, water;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        HandleInputs();

        if (transform.position != Destination)
        {
            if (Vector3.Dot(Direction, Destination - this.transform.position) > 0)
                transform.position += Direction * MoveSpeed * Time.deltaTime;
            else
                transform.position = Destination;
        }
    }

    void HandleInputs()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (transform.position == Destination)
        {
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
                    Destination += Direction * CaseSize;
            }
            else
                pressedTimeElapsed = 0;

            if (newDirection != Vector3.zero)
                Direction = newDirection;
        }
    }

    void Reset()
    {
        Direction = Vector3.right;
        Destination = transform.position;
        pressedTimeElapsed = 0;

        Lifes = 5;
        Rocks = 0;
        IsBucketFilled = false;
    }

    void Die()
    {
        --Lifes;
        GetComponent<AudioSource>().PlayOneShot(Random.value > 0.5 ? dead : trash_dead, 1.0f);
    }

    bool IsGameOver()
    {
        return Lifes <= 0;
    }

    bool IsInventoryFull()
    {
        return Rocks >= 3;
    }

    bool IsInventoryEmpty()
    {
        return Rocks <= 0;
    }

    void FillBucket()
    {
        GetComponent<AudioSource>().PlayOneShot(water, 1.0f);
        IsBucketFilled = true;
    }

    void EmptyBucket()
    {
        IsBucketFilled = false;
    }

    void AddRockToInventory()
    {
        if (IsInventoryFull())
            throw new InvalidOperationException("Can't add a stone to full inventory ! Check inventory status before.");

        ++Rocks;
    }

    void RemoveRockFromInventory()
    {
        if (IsInventoryEmpty())
            throw new InvalidOperationException("Can't remove a stone from empty inventory ! Check inventory status before.");

        --Rocks;
    }
}

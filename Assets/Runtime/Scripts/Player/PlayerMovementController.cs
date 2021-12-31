using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    PlayerInputController playerInput;

    [Header("Movement X")]
    [SerializeField] float forwardSpeed = 0.5f;

    [Header("Jump")]
    [SerializeField] int numberOfJumps = 2;
    [SerializeField] float jumpDistanceX = 4;
    [SerializeField] float jumpHeightY = 2;
    float jumpStartX;
    float jumpStartY;
    Vector2 initialPosition;
    int currentJumps;
    public bool IsJumping { get; private set; }
    public bool IsRun { get; private set; }
    public float JumpDuration => jumpDistanceX / forwardSpeed;
    bool stopMove = false;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInputController>();
        currentJumps = numberOfJumps;
    }

    void FixedUpdate()
    {
        MoveForward();
    }

    void Update()
    {
        ProcessInput();
        Vector2 position = transform.position;
        position.y = ProcessJump();
        transform.position = position;
    }

    void MoveForward()
    {
        if (stopMove)
        {
            return;
        }

        Vector2 position = transform.position;
        position += Vector2.right * forwardSpeed * Time.fixedDeltaTime;
        transform.position = position;
    }

    void ProcessInput()
    {
        IsJumping = false;

        if (playerInput.IsJump && currentJumps > 0 && !stopMove)
        {
            jumpStartX = transform.position.x;
            jumpStartY = transform.position.y;
            currentJumps--;
            IsJumping = true;
            IsRun = false;
        }
    }

    float ProcessJump()
    {
        float deltaY = 0;
        float jumpCurrentProgress = transform.position.x - jumpStartX;
        float jumpPercent = jumpCurrentProgress / jumpDistanceX;

        if (jumpPercent >= 1)
        {
            currentJumps = numberOfJumps;
            IsRun = true;
        }
        else
        {
            deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
        }

        return Mathf.Lerp(jumpStartY, initialPosition.y + deltaY, jumpPercent);
    }

    public void StopMove()
    {
        stopMove = true;
    }
}

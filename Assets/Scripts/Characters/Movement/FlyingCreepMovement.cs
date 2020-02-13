using UnityEngine;

public class FlyingCreepMovement : Movement
{
    public override float moveSpeed { get; set; }

    CharacterController characterController;
    float movingTime;
    float waitBefore;
    float waitAfterMove;

    float timer;
    Vector3 direction;

    enum State { idle, isMoving, isWaitingAfter }
    State currentState = State.idle;

    public override bool inProgress { get; set; }


    public FlyingCreepMovement(CharacterController characterController, float moveSpeed, float movingTime, float waitAfter)
    {
        this.characterController = characterController;
        this.moveSpeed = moveSpeed;
        this.movingTime = movingTime;
        waitAfterMove = waitAfter;

        inProgress = true;
    }

    public override void DoMove()
    {

        switch (currentState)
        {
            case State.idle:
                timer = movingTime;
                currentState = State.isMoving;
                DefineDirection();

                //look rotate
                characterController.transform.LookAt(characterController.transform.position + direction);
                break;

            case State.isMoving:
                if(timer > 0)
                {
                    characterController.Move(direction * moveSpeed * Time.fixedDeltaTime);
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    timer = waitAfterMove;
                    currentState = State.isWaitingAfter;
                }
                break;

            case State.isWaitingAfter:
                if(timer > 0)
                {
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    inProgress = false;
                    currentState = State.idle;
                }
                break;
        }
    }

    public void StopMove()
    {
        if (currentState == State.isMoving)
            currentState = State.isWaitingAfter;
    }

    void DefineDirection()
    {
        direction = Random.insideUnitSphere.normalized;
        direction = new Vector3(direction.x, 0, direction.z);
    }
}

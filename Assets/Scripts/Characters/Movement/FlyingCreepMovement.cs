using UnityEngine;

public class FlyingCreepMovement : Movement
{
    public override IMoveSettings moveSettings { get; set; }

    CharacterController characterController;
    float timer;
    Vector3 direction;

    enum State { idle, isMoving, isWaitingAfter }
    State currentState = State.idle;

    public override bool inProgress { get; set; }

    public FlyingCreepMovement(ISettings settings, CharacterController characterController)
    {
        moveSettings = (IMoveSettings)settings;
        this.characterController = characterController;

        inProgress = true;
    }

    public override void DoMove()
    {
        switch (currentState)
        {
            case State.idle:
                timer = moveSettings.MovingTime;
                currentState = State.isMoving;
                DefineDirection();

                //look rotate
                characterController.transform.LookAt(characterController.transform.position + direction);
                break;

            case State.isMoving:
                if(timer > 0)
                {
                    characterController.Move(direction * moveSettings.MoveSpeed * Time.fixedDeltaTime);
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    timer = moveSettings.WaitAfterMove;
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

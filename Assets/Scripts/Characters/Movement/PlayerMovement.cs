using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public override float moveSpeed { get; set; }
    public override bool inProgress { get; set; }

    CharacterController characterController;
    Joystick joystick;

    public PlayerMovement(CharacterController characterController, Joystick joystick, float moveSpeed)
    {
        this.characterController = characterController;
        this.joystick = joystick;
        this.moveSpeed = moveSpeed;
    }

    public override void DoMove()
    {
        var direction = new Vector3(joystick.horizontalDirection, 0, joystick.verticalDirection);

        characterController.Move(direction * moveSpeed * Time.fixedDeltaTime);
        characterController.transform.LookAt(characterController.transform.position + direction);
    }
}

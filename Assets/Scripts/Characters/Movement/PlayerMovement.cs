using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public override IMoveSettings moveSettings { get; set; }
    public override bool inProgress { get; set; }

    CharacterController characterController;
    Joystick joystick;

    public PlayerMovement(ISettings settings,  CharacterController characterController, Joystick joystick)
    {
        moveSettings = (IMoveSettings)settings;
        this.characterController = characterController;
        this.joystick = joystick;
    }

    public override void DoMove()
    {
        var direction = new Vector3(joystick.horizontalDirection, 0, joystick.verticalDirection);

        characterController.Move(direction * moveSettings.MoveSpeed * Time.fixedDeltaTime);
        characterController.transform.LookAt(characterController.transform.position + direction);
    }
}

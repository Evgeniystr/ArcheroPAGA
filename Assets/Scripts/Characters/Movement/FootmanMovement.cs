using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootmanMovement : Movement
{
    public override float moveSpeed { get; set; }
    public override bool inProgress { get; set; }

    NavMeshAgent agent;

    public FootmanMovement(NavMeshAgent agent, float moveSpeed, float rotateSpeed)
    {
        this.agent = agent;
        this.agent.speed = moveSpeed;
        this.agent.angularSpeed = rotateSpeed;
    }


    public override void DoMove()
    {
        var target = SpawnControler.Instance.playerOnScene;

        if (target.activeSelf)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}

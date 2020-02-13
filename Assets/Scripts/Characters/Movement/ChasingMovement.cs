using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingMovement : Movement
{
    public override IMoveSettings moveSettings { get; set; }
    public override bool inProgress { get; set; }

    NavMeshAgent agent;

    public ChasingMovement(ISettings settings, NavMeshAgent agent)
    {
        moveSettings = (IMoveSettings)settings;
        this.agent = agent;
        this.agent.speed = moveSettings.MoveSpeed;
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

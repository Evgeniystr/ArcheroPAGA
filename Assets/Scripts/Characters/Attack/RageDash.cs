using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageDash : Attack
{
    public override IAttackSettings attackSettings { get; set; }
    public override float currentCooldown { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }

    IMoveSettings moveSettings;
    CharacterController characterController;
    Vector3 target;
    Vector3 direction;
    float timer;
    float attackCooldown;

    public override bool inProgress { get; set; }

    enum State { Seting, Moving, Waiting }
    State currentState;

    public RageDash(ISettings settings, CharacterController characterController, ShootAt shootAt)
    {
        attackSettings = (IAttackSettings)settings;
        moveSettings = (IMoveSettings)settings;
        this.characterController = characterController;
        this.shootAt = shootAt;

        currentState = State.Seting;
        inProgress = false;
    }


    public override void DoAttack()
    {
        switch (currentState)
        {
            case State.Seting:
                GetTarhetAndDirection(out target, out direction);

                characterController.transform.LookAt(target);
                timer = moveSettings.MovingTime;
                                               
                currentState = State.Moving;
                break;

            case State.Moving:
                if(timer > 0)
                {
                    timer -= Time.fixedDeltaTime;
                    characterController.Move(direction * (moveSettings.MoveSpeed*2) * Time.deltaTime);
                }
                else
                {
                    timer = moveSettings.WaitAfterMove;
                    currentState = State.Waiting;
                }
                break;

            case State.Waiting:
                if(timer > 0)
                {
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    currentState = State.Seting;
                    inProgress = false;
                }
                break;
        }

        if (attackCooldown > 0)
            attackCooldown -= Time.fixedDeltaTime;
        
    }

    public void DealDamage(ControllerColliderHit hit)
    {
        Debug.Log(1);
        if(attackCooldown <= 0)
        {
            if(shootAt == ShootAt.player && hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.gameObject.GetComponent<ICharacter>().TakeDamage(attackSettings.Damage);
            }
            else if(shootAt == ShootAt.enemies && hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                hit.gameObject.GetComponent<ICharacter>().TakeDamage(attackSettings.Damage);
            }

            attackCooldown = 1;
        }
    }

    void GetTarhetAndDirection(out Vector3 target, out Vector3 direction)
    {
        target = SpawnControler.Instance.GetClosestTarget(shootAt).transform.position;
        target = new Vector3(target.x, characterController.transform.position.y, target.z);

        direction = new Vector3(target.x - characterController.transform.position.x, -2, target.z - characterController.transform.position.z).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Boss : Behaviour, ICharacter
{
    [SerializeField]
    CharacterController characterController;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Pool projectilePool;

    //Behaviour
    public override Movement movement { get; set; }
    public override Attack attack { get; set; }

    //ICharacter
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    enum BossBehaviourState { Dash, RapidFire, SpreadFire}
    BossBehaviourState currentState;

    BossSettings settings;


    private void Start()
    {
        if(characterController == null)
            characterController = GetComponent<CharacterController>();
        if (projectilePool == null)
            projectilePool = GetComponent<Pool>();

        settings = SettingsLoader.LoadSettings<BossSettings>();

        MaxHealth = settings.Health;
        CurrentHealth = MaxHealth;

        SetBehaviour();
    }


    private void FixedUpdate()
    {
        BehavoiurExecute();
    }


    public override void BehavoiurExecute()
    {
        if (attack != null && attack.inProgress)
        {
            attack.DoAttack();
        }
        else if(movement != null && movement.inProgress)
        {
            movement.DoMove();
        }
        else
        {
            SetBehaviour();
        }
    }

    void SetBehaviour()
    {
        ChangeState();
        ChangeBehaviour();
    }


    void ChangeState()
    {
        var enumValues = Enum.GetValues(typeof(BossBehaviourState));
        var random = new System.Random();
        var ind = random.Next(0, enumValues.Length);
        currentState = (BossBehaviourState)enumValues.GetValue(ind);
    }

    void ChangeBehaviour()
    {
        switch (currentState)
        {
            case BossBehaviourState.Dash:
                attack = new RageDash(settings, characterController, ShootAt.player);
                break;
            case BossBehaviourState.RapidFire:
                attack = new RapidShotAttack(settings, firePoint, projectilePool, ShootAt.player);
                movement = new RandomeDirectionMovement(settings, characterController);
                break;
            case BossBehaviourState.SpreadFire:
                attack = new SpreadAttack(settings, firePoint, projectilePool, ShootAt.player);
                break;
        }

        attack.inProgress = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(currentState == BossBehaviourState.Dash)
        {
            var rd = (RageDash)attack;
            rd.DealDamage(hit);
        }
    }

    public void Die()
    {
        SpawnControler.Instance.CreatureDie(gameObject, CreatureType.Boss);
    }

    public void TakeDamage(int damageValue)
    {
        CurrentHealth -= damageValue;

        if (CurrentHealth <= 0)
            Die();
    }
}

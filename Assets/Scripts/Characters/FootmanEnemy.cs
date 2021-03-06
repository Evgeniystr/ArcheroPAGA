﻿using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class FootmanEnemy : Behaviour, ICharacter
{
    [SerializeField]
    NavMeshAgent agent;

    //ICharacter
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    //Behaviour
    public override Movement movement { get; set; }
    public override Attack attack { get; set; }


    private void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        var settings = SettingsLoader.LoadSettings<FootmanSettings>();

        movement = new ChasingMovement(settings, agent);
        attack = new TouchAttack(settings, ShootAt.player);

        MaxHealth = settings.Health;
        CurrentHealth = MaxHealth;
    }

    private void FixedUpdate()
    {
        BehavoiurExecute();
    }

    private void OnTriggerStay(Collider other)
    {
        attack.DoAttack(other);
    }

    public override void BehavoiurExecute()
    {
        movement.DoMove();
    }


    public void TakeDamage(int damageValue)
    {
        CurrentHealth -= damageValue;

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        SpawnControler.Instance.CreatureDie(gameObject, CreatureType.Common);
    }
}

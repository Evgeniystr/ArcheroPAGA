using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FlyingCreep : Behaviour, ICharacter
{
    [SerializeField]
    CharacterController characterController;
    [SerializeField]
    Pool projectilePool;
    [SerializeField]
    Transform firePoint;

    //Behaviour
    public override Movement movement { get; set; }
    public override Attack attack { get; set; }

    //ICharacter
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }


    private void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
        if (projectilePool == null)
            projectilePool = GetComponent<Pool>();

        var settings = SettingsLoader.LoadSettings<FlyingCreepSettings>();
        movement = new FlyingCreepMovement(settings, characterController);
        attack = new RapidShotAttack(settings, firePoint, projectilePool, ShootAt.player);


        //set forst action
        movement.inProgress = true;

        MaxHealth = settings.Health;
        CurrentHealth = MaxHealth;
    }

    private void FixedUpdate()
    {
        BehavoiurExecute();
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

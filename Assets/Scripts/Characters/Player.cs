using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : Behaviour, ICharacter
{
    [SerializeField]
    CharacterController characterController;
    [SerializeField]
    Joystick joystick;
    [SerializeField]
    Pool projectilePool;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Text healthText;

    //ICharacter
    public int MaxHealth { get; set;}
    public int CurrentHealth { get; set; }

    //Behaviour
    public override Movement movement { get; set; }
    public override Attack attack { get; set; }


    private void Start()
    {
        if (projectilePool == null)
            projectilePool = GetComponent<Pool>();

        var settings = SettingsLoader.LoadSettings<PlayerSettings>();

        MaxHealth = settings.Health;
        CurrentHealth = MaxHealth;

        movement = new PlayerMovement(characterController, joystick, settings.MoveSpeed);
        attack = new RapidShotAttack(settings.RateOfFire, settings.Damage, settings.ProjectileSpeed, 
                                     firePoint, projectilePool, gameObject, ShootAt.enemies);

        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        UpdateHelthbar();
    }

    private void FixedUpdate()
    {
        BehavoiurExecute();
    }


    public override void BehavoiurExecute()
    {
        if(joystick.isInputActive)
        {
            movement.DoMove();
        }
        else
        {
            attack.DoAttack();
        }
    }

    void UpdateHelthbar()
    {
        var newValue = (1f / MaxHealth) * CurrentHealth;
        healthSlider.value = newValue;

        healthText.text = string.Format("{0}/{1}", CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int damageValue)
    {
        CurrentHealth -= damageValue;
        UpdateHelthbar();

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.LevelEnd(LevelResult.Lose);
        print("Player dead");//
    }
}

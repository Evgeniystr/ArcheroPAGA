using UnityEngine;

public class RapidShotAttack : Attack
{
    public override IAttackSettings attackSettings { get; set; }
    public override float currentCooldown { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }
    double currentBurst;

    public override bool inProgress { get; set; }

    Transform thisCharacter;
    float timer;
    enum State { isShooting, isWaiting }
    State currentState;


    public RapidShotAttack(ISettings settings, Transform firePoint, Pool projectilePool, ShootAt shootAt)
    {
        attackSettings = (IAttackSettings)settings;
        this.firePoint = firePoint;
        this.projectilePool = projectilePool;
        this.shootAt = shootAt;

        thisCharacter = firePoint.root;
        currentState = State.isShooting;
    }


    public override void DoAttack()
    {
        switch (currentState)
        {
            case State.isShooting:
                Shooting();
                break;
            case State.isWaiting:
                Wait();
                break;
        }
    }

    void Shooting()
    {
        var targetGO = SpawnControler.Instance.GetClosestTarget(shootAt);
        if (targetGO == null)
            return;

        //rotate
        var lookTarget = new Vector3(targetGO.transform.position.x, thisCharacter.position.y, targetGO.transform.position.z);
        thisCharacter.LookAt(lookTarget);

        //shoot
        if (CooldownTimer())
        {
            var newProjectile = projectilePool.GetPoolItem();
            newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targetGO.transform.position, 
                                                      attackSettings.Damage, attackSettings.ProjectileSpeed, shootAt);

            //set cooldown
            currentCooldown = attackSettings.RateOfFire;

            currentBurst++;
            BurstLimitCheck();
        }
    }

    void BurstLimitCheck()
    {
        if (currentBurst == attackSettings.BurstCount)
        {
            currentBurst = 0;
            timer = attackSettings.WaitAfterShoot;
            currentState = State.isWaiting;
        }
    }

    void Wait()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            currentState = State.isShooting;
            inProgress = false;
        }
    }
}

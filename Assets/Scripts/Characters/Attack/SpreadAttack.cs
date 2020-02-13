using UnityEngine;

public class SpreadAttack : Attack
{
    public override IAttackSettings attackSettings { get; set; }
    public override float currentCooldown { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }

    enum State { isShooting, isWaiting }
    State currentState;
    Transform thisCharacter;
    float timer;

    public override bool inProgress { get; set; }

    public SpreadAttack(ISettings settings, Transform firePoint, Pool projectilePool, ShootAt shootAt)
    {
        attackSettings = (IAttackSettings)settings;
        this.firePoint = firePoint;
        this.projectilePool = projectilePool;
        this.shootAt = shootAt;
        thisCharacter = firePoint.root;
    }

    public override void DoAttack()
    {
        switch (currentState)
        {
            case State.isShooting:
                Shot();
                break;
            case State.isWaiting:
                Wait();
                break;
        }
    }

    void Shot()
    {
        Transform targeTransform;

        try
        {
            targeTransform = SpawnControler.Instance.GetClosestTarget(shootAt).transform;
        }
        catch
        {
            return;
        }

        //rotate
        var lookTarget = new Vector3(targeTransform.position.x, thisCharacter.position.y, targeTransform.position.z);
        thisCharacter.LookAt(lookTarget);

        if(attackSettings.SpreadCount == 1)
        {
            var newProjectile = projectilePool.GetPoolItem();
            newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targeTransform.position,
                                                      attackSettings.Damage, attackSettings.ProjectileSpeed, shootAt);
        }
        else
        {
            MakeSpread(targeTransform);
        }

        timer = attackSettings.WaitAfterShoot;
        currentState = State.isWaiting;
    }

    void MakeSpread(Transform targeTransform)
    {
        var rotateModifyer = -(attackSettings.SpreadCount / 2) * attackSettings.SpreadStep;//starting modifyer

        for (int i = 0; i < attackSettings.SpreadCount; i++)
        {
            var newProjectile = projectilePool.GetPoolItem();
            newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targeTransform.position,
                                                      attackSettings.Damage, attackSettings.ProjectileSpeed, shootAt, rotateModifyer);
            rotateModifyer += attackSettings.SpreadStep;
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

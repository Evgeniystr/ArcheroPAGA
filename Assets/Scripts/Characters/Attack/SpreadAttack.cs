using UnityEngine;

public class SpreadAttack : Attack
{
    public override float rateOfFire { get; set; }
    public override float waitAfterShoot { get; set; }
    public override float speed { get; set; }
    public override float currentCooldown { get; set; }
    public override int damage { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }

    enum State { isShooting, isWaiting }
    State currentState;
    Transform thisCharacter;
    int spreadCount;
    float timer;
    float spreadStep;

    public override bool inProgress { get; set; }


    public SpreadAttack(int damage, float speed, Transform firePoint, Pool projectilePool, float waitAfterShoot, ShootAt shootAt, int spreadCount = 1, float spreadStep = 30)
    {
        this.damage = damage;
        this.speed = speed;
        this.firePoint = firePoint;
        this.projectilePool = projectilePool;
        this.waitAfterShoot = waitAfterShoot;
        this.shootAt = shootAt;
        this.spreadCount = spreadCount;
        thisCharacter = firePoint.root;
        this.spreadStep = spreadStep;
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

        if(spreadCount == 1)
        {
            var newProjectile = projectilePool.GetPoolItem();
            newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targeTransform.position, damage, speed, shootAt);
        }
        else
        {
            MakeSpread(targeTransform);
        }

        timer = waitAfterShoot;
        currentState = State.isWaiting;
    }

    void MakeSpread(Transform targeTransform)
    {
        var rotateModifyer = -(spreadCount / 2) * spreadStep;//starting modifyer

        for (int i = 0; i < spreadCount; i++)
        {
            var newProjectile = projectilePool.GetPoolItem();
            newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targeTransform.position, damage, speed, shootAt, rotateModifyer);
            rotateModifyer += spreadStep;
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

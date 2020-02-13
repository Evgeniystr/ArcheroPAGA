using UnityEngine;

public class RapidShotAttack : Attack
{
    public override int damage { get; set; }
    public override float rateOfFire { get; set; }
    public override float speed { get; set; }
    public override float waitAfterShoot { get; set; }
    public override float currentCooldown { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }
    GameObject thisCharacterGO { get; set; }
    double burst;
    double currentBurst;

    public override bool inProgress { get; set; }


    public RapidShotAttack(float rateOfFire, int damage, float speed, Transform firePoint, Pool projectilePool, 
                            GameObject thisCharacterGO, ShootAt shootAt, double burst = double.PositiveInfinity )
    {
        this.rateOfFire = rateOfFire;
        this.speed = speed;
        this.waitAfterShoot = waitAfterShoot;
        this.damage = damage;
        this.firePoint = firePoint;
        this.projectilePool = projectilePool;
        this.thisCharacterGO = thisCharacterGO;
        this.shootAt = shootAt;
        this.burst = burst;
    }


    public override void DoAttack()
    {
        var targetGO = SpawnControler.Instance.GetClosestTarget(shootAt);
        if (targetGO == null)
            return;

        //rotate
        var lookTarget = new Vector3(targetGO.transform.position.x, thisCharacterGO.transform.position.y, targetGO.transform.position.z);
        thisCharacterGO.transform.LookAt(lookTarget);

        //shoot
        if (CooldownTimer())
        {
            var newProjectile = projectilePool.GetPoolItem();
            newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targetGO.transform.position, damage, speed, shootAt);

            //set cooldown
            currentCooldown = rateOfFire;

            currentBurst++;
            BurstLimitCheck();
        }
    }

    void BurstLimitCheck()
    {
        if(currentBurst == burst)
        {
            currentBurst = 0;
            inProgress = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotAttack : Attack
{
    public override int damage { get; set; }
    public override float rateOfFire { get; set; }
    public override float speed { get; set; }
    public override float waitAfterShoot { get; set; }
    public override float currentCooldown { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }

    public override bool inProgress { get; set; }

    Transform thisCharacter;
    float timer;
    enum State { isShooting, isWaiting }
    State currentState;


    public SingleShotAttack(int damage, float speed, Transform firePoint, Pool projectilePool, float waitAfterShoot, ShootAt shootAt)
    {
        this.speed = speed;
        this.damage = damage;
        this.firePoint = firePoint;
        this.projectilePool = projectilePool;
        this.waitAfterShoot = waitAfterShoot;
        this.shootAt = shootAt;

        thisCharacter = firePoint.root;
        currentState = State.isShooting;
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

        var newProjectile = projectilePool.GetPoolItem();
        newProjectile.GetComponent<IProjectile>().SetAndShoot(firePoint.position, targeTransform.position, damage, speed, shootAt);

        timer = waitAfterShoot;
        currentState = State.isWaiting;
    }

    void Wait()
    {
        if(timer > 0)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttack : Attack
{
    public override float rateOfFire { get; set; }
    public override float waitAfterShoot { get; set; }
    public override float speed { get; set; }
    public override float currentCooldown { get; set; }
    public override int damage { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }

    public override bool inProgress { get; set; }

    public TouchAttack(int damage, float rateOfFire, ShootAt shootAt)
    {
        this.damage = damage;
        this.rateOfFire = rateOfFire;
        this.shootAt = shootAt;
    }

    

    public override void DoAttack(Collider other)
    {
        if (OponentDetect(other.gameObject.layer) && CooldownTimer())
        {
            other.gameObject.GetComponent<ICharacter>().TakeDamage(damage);

            //cooldown
            currentCooldown = rateOfFire;
        }
    }


    bool OponentDetect(int otherLayer)
    {
        switch (shootAt)
        {
            case ShootAt.enemies:
                return LayerMask.NameToLayer("Enemy") == otherLayer;

            case ShootAt.player:
                return LayerMask.NameToLayer("Player") == otherLayer;

            default:
                return false;
        }
    }


    public override void DoAttack(){}
}

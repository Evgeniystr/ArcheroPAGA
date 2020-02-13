using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttack : Attack
{
    public override IAttackSettings attackSettings { get; set; }
    public override float currentCooldown { get; set; }
    public override Transform firePoint { get; set; }
    public override Pool projectilePool { get; set; }
    public override ShootAt shootAt { get; set; }

    public override bool inProgress { get; set; }

    public TouchAttack(ISettings settings, ShootAt shootAt)
    {
        attackSettings = (IAttackSettings)settings;
        this.shootAt = shootAt;
    }

    

    public override void DoAttack(Collider other)
    {
        if (OponentDetect(other.gameObject.layer) && CooldownTimer())
        {
            other.gameObject.GetComponent<ICharacter>().TakeDamage(attackSettings.Damage);

            //cooldown
            currentCooldown = attackSettings.RateOfFire;
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

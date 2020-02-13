using UnityEngine;

public enum ShootAt { player, enemies }

public abstract class Attack
{
    public abstract float rateOfFire { get; set; }
    public abstract float waitAfterShoot { get; set; }
    public abstract float speed { get; set; }
    public abstract float currentCooldown { get; set; }
    public abstract int damage { get; set; }
    public abstract Transform firePoint { get; set; }
    public abstract Pool projectilePool { get; set; }
    public abstract ShootAt shootAt { get; set; }
    public abstract bool inProgress { get; set; }


    public virtual void DoAttack()
    {
        throw new System.NotImplementedException();
    }

    public virtual void DoAttack(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public bool CooldownTimer()
    {
        if(currentCooldown <= 0)
        {
            return true;
        }
        else
        {
            currentCooldown -= Time.fixedDeltaTime;
            return false;
        }
    }

    public void ChangeProjectile(GameObject projectileGO)
    {
        projectilePool.SetPoolItem(projectileGO);
    }
}

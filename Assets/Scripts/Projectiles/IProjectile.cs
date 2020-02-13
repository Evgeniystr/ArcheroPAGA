using UnityEngine;

public interface IProjectile
{
    void SetAndShoot(Vector3 position, Vector3 direction, int damage, float speed, ShootAt shootAt, float rotateModify = 0);
}

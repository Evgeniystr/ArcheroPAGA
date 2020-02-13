using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    [SerializeField] float speed = 1;
    [SerializeField] float destroyTimer = 5;
    [SerializeField] Material playerProjectileMat;
    [SerializeField] Material enemyProjectileMat;
    int damage;
    Vector3 direction;
    ShootAt shootAt;


    private void Update()
    {
        Fly();
    }


    public void SetAndShoot(Vector3 position, Vector3 direction, int damage, float speed, ShootAt shootAt, float rotateModify = 0)
    {
        this.damage = damage;
        this.speed = speed;
        this.direction = direction.normalized;
        this.shootAt = shootAt;

        transform.position = position;
        transform.LookAt(direction);

        //rotate modify
        transform.Rotate(Vector3.up, rotateModify);

        gameObject.SetActive(true);
        StartCoroutine(DestroyTimer());
    }

    void Fly()
    {
        var velocity = transform.forward * speed * Time.deltaTime;
        transform.Translate(velocity, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        var isOponentDetected = OponentDetect(other.gameObject.layer);

        if(LayerMask.NameToLayer("Obstacle") == other.gameObject.layer)
        {
            gameObject.SetActive(false);

        }
        else if (isOponentDetected)
        {
            var character = other.gameObject.GetComponent<ICharacter>();
            character.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        else
        {
            return;
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


    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy();
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}

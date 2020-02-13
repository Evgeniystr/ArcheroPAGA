using System;

[Serializable]
public class PlayerSettings : ISettings
{
    public float MoveSpeed;
    public int Health;
    public int Damage;
    public float RateOfFire;
    public float ProjectileSpeed;
    public float WaitAfterShoot;
}

using System;

[Serializable]
public class PlayerSettings : ISettings, IAttackSettings, IMoveSettings
{
    public int Health;

    //IMoveSettings
    public float MoveSpeed { get; set; }
    public float MovingTime { get; set; }
    public float WaitAfterMove { get; set; }

    //IAttackSettings
    public int Damage { get; set; }
    public float RateOfFire { get; set; }
    public float ProjectileSpeed { get; set; }
    public float WaitAfterShoot { get; set; }
    public int BurstCount { get; set; }
    public int SpreadStep { get; set; }
    public int SpreadCount { get; set; }
}

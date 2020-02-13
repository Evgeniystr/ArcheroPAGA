public interface IAttackSettings
{
    int Damage { get; set; }
    float RateOfFire { get; set; }
    float ProjectileSpeed { get; set; }
    float WaitAfterShoot { get; set; }
    int BurstCount { get; set; }
    int SpreadStep { get; set; }
    int SpreadCount { get; set; }
}

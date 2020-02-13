public interface IAttackSettings
{
    int Damage { get; set; }
    float RateOfFire { get; set; }
    float ProjectileSpeed { get; set; }
    float WaitBeforShoot { get; set; }
    float WaitAfterShoot { get; set; }
}

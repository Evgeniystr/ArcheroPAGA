﻿using System;

[Serializable]
public class BossSettings : ISettings
{
    public float MoveSpeed;
    public int Health;
    public float MovingTime;
    public float WaitAfterMove;
    public int Damage;
    public float RateOfFire;
    public float ProjectileSpeed;
    public float WaitAfterShoot;
    public double BurstCount;
}
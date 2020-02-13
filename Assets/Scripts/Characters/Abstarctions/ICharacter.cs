using System;

interface ICharacter
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }

    void TakeDamage(int damageValue);
    void Die();
}

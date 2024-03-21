using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void Damage(int damageAmount, int damageReason = -1);
    public void Die();

    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
}

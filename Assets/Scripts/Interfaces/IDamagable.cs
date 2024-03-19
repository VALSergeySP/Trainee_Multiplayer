using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void Damage(float damageAmount);
    public void Die();

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
}

using Fusion;

public class PlayerHealthController : NetworkBehaviour, IDamagable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Death logic
    }
}

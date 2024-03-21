using Fusion;
using UnityEngine;

public class PlayerHealthController : NetworkBehaviour, IDamagable
{
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath OnPlayerDeathEvent;

    private ChangeDetector _changeDetector;
    private UIPlayerHealthManager _healthUI;

    [field: SerializeField] public int MaxHealth { get; set; }
    [Networked] public int CurrentHealth { get; set; }

    /*
    [Networked] 
    private NetworkBool _networkIsDead { get; set; }*/

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        CurrentHealth = MaxHealth;

        if(Object.HasInputAuthority)
        {
            _healthUI = FindAnyObjectByType<UIPlayerHealthManager>(FindObjectsInactive.Include);
            _healthUI.SetHealth(CurrentHealth, MaxHealth);
        }
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(CurrentHealth):
                    if (_healthUI != null)
                    {
                        _healthUI.SetHealth(CurrentHealth, MaxHealth);
                    }
                    break;
            }
        }
    }

    public void Damage(int damageAmount, int enemy)
    {
        CurrentHealth -= damageAmount;
        Debug.Log($"Player was damaged! HP: {CurrentHealth}, Damage: {damageAmount}");

        if (Object.HasInputAuthority)
        {
            _healthUI.SetHealth(CurrentHealth, MaxHealth);
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnPlayerDeathEvent?.Invoke();
    }
}

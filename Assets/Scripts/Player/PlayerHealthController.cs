using Fusion;
using UnityEngine;

public class PlayerHealthController : NetworkBehaviour, IDamagable
{
    private Animator _animator;
    private int _hitTrigger = Animator.StringToHash("Hit");
    private int _healTrigger = Animator.StringToHash("Heal");

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
        _animator = GetComponent<Animator>();
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

        if (Object.HasInputAuthority)
        {
            _healthUI.SetHealth(CurrentHealth, MaxHealth);
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Healing(int healAmount)
    {
        CurrentHealth += healAmount;

        if (CurrentHealth > MaxHealth) { CurrentHealth = MaxHealth; }

        if (Object.HasInputAuthority)
        {
            _healthUI.SetHealth(CurrentHealth, MaxHealth);
        }
    }

    public void Die()
    {
        OnPlayerDeathEvent?.Invoke();
    }
}

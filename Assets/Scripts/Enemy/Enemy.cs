using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : NetworkBehaviour, IDamagable, IEnemyMovable
{
    private Animator _animator;
    private int _hitIndex = Animator.StringToHash("Hit");
    private int _deadIndex = Animator.StringToHash("Dead");


    [field: SerializeField] public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;

    public bool IsWithinAttackDistance { get; set; } = false;

    public EnemyStateMachine StateMachineInstance { get; set; }
    
    public EnemyIdleState IdleState { get; set; }
    public EnemyAttackState AttackState { get; set; }


    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private int _enemyDamage = 10;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachineInstance = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachineInstance);
        AttackState = new EnemyAttackState(this, StateMachineInstance);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachineInstance.Initialize(IdleState);
    }

    public override void FixedUpdateNetwork()
    {
        StateMachineInstance.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachineInstance.CurrentState.PhysicsUpdate();
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180f, transform.rotation.eulerAngles.z);
            IsFacingRight = false;
        } 
        else if (!IsFacingRight && velocity.x > 0f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);
            IsFacingRight = true;
        }
    }
    public void AnimationTriggerEvent(AnimationTriggerType type)
    {
        EnemyState currentState = (EnemyState)StateMachineInstance.CurrentState;
        if (currentState != null)
        {
            currentState.AnimationTriggerEvent(type);
        }
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        EnemyKilled
    }

    public void Damage(int damageAmount, int damageCauseByPlayerId)
    {
        CurrentHealth -= damageAmount;

        _animator.SetTrigger(_hitIndex);
        
        if(damageCauseByPlayerId > 0)
        {
            EnemiesSpawner.Instance.RecieveDamageFromPlayer(damageAmount, damageCauseByPlayerId);
        }

        if (CurrentHealth <= 0)
        {
            Die();
            if (damageCauseByPlayerId > 0)
            {
                EnemiesSpawner.Instance.KilledByPlayer(damageCauseByPlayerId);
            }
        }
    }

    public void Die()
    {
        RB.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        _animator.SetBool(_deadIndex, true);

        DespawnEnemy();
    }

    private void DespawnEnemy()
    {
        Runner.Despawn(Object);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;

        CheckForLeftOrRightFacing(velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(_enemyDamage, 0);
        }
    }
}

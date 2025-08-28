using Fusion;
using UnityEngine;

public class Enemy : NetworkBehaviour, IDamagable, IEnemyMovable
{
    private Animator _animator;
    private readonly int _hitIndex = Animator.StringToHash("Hit");
    private readonly int _deadIndex = Animator.StringToHash("Dead");

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        EnemyKilled
    }


    private EnemiesSpawner _spawner;
    public EnemiesSpawner Spawner { get => _spawner; }


    // State machine
    public EnemyStateMachine StateMachineInstance { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; private set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; private set; }

    public bool IsWithinAttackDistance { get; set; } = false;
    [SerializeField] private int _enemyDamage = 10;


    public void Init(EnemiesSpawner spawner)
    {
        _spawner = spawner;
    }

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(_enemyDamage);
        }
    }


    // IEnemyMovable
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;

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

    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;

        CheckForLeftOrRightFacing(velocity);
    }


    // IDamagable
    [field: SerializeField] public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }

    public void Damage(int damageAmount, int damageCauseByPlayerId)
    {
        CurrentHealth -= damageAmount;

        _animator.SetTrigger(_hitIndex);
        
        if(damageCauseByPlayerId > 0)
        {
            _spawner.RecieveDamageFromPlayer(damageAmount, damageCauseByPlayerId);
        }

        if (CurrentHealth <= 0)
        {
            Die();

            if (damageCauseByPlayerId > 0)
            {
                _spawner.KilledByPlayer(damageCauseByPlayerId);
            }
        }
    }

    public void Die()
    {
        RB.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        _animator.SetBool(_deadIndex, true);

        Runner.Despawn(Object);
    }
}

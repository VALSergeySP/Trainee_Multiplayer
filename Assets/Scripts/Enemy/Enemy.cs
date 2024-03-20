using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMovable
{
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
        RB = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachineInstance.Initialize(IdleState);
    }

    private void Update()
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
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        } 
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
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
    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;

        CheckForLeftOrRightFacing(velocity);
    }
}

using Fusion;
using UnityEngine;

public class BulletProjectile : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";


    [SerializeField] private bool _canDamagePlayer = false;
    [SerializeField] private float _startRotation = -90f; // If sprite directed in different direction (defauls: right)

    [SerializeField] private int _bulletDamage;
    public int Damage { get => _bulletDamage; }


    [SerializeField] private float _bulletSpeed = 5f;
    public float BulletSpeed { get => _bulletSpeed; }


    private Vector2 _movementDirection;
    private float _angle;


    [Networked] private TickTimer Life { get; set; }


    public void Init(float angle, float despawnTime)
    {
        _angle = angle + _startRotation;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        _movementDirection = transform.up;

        Life = TickTimer.CreateFromSeconds(Runner, despawnTime);
    }


    public override void FixedUpdateNetwork()
    {
        if (Life.Expired(Runner))
            Runner.Despawn(Object);
        else
            transform.position += _bulletSpeed * Runner.DeltaTime * (Vector3)_movementDirection;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ENEMY_TAG) && !_canDamagePlayer)
        {
            int playerId = Object.InputAuthority.PlayerId;
            collision.GetComponent<IDamagable>().Damage(_bulletDamage, playerId);
            Runner.Despawn(Object);
        }

        if(collision.CompareTag(PLAYER_TAG) && _canDamagePlayer)
        {
            collision.GetComponent<IDamagable>().Damage(_bulletDamage);
            Runner.Despawn(Object);
        }
    }
}

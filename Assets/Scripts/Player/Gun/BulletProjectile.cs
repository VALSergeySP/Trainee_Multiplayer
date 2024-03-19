using Fusion;
using UnityEngine;

public class BulletProjectile : NetworkBehaviour
{
    [SerializeField] private float _bulletDamage;
    public float Damage { get => _bulletDamage; }
    [SerializeField] private float _bulletSpeed = 5f;
    public float BulletSpeed { get => _bulletSpeed; }
    [SerializeField] private float _destroyTime = 5f;

    private Vector2 _movementDirection;
    private float _angle;

    [Networked] private TickTimer life { get; set; }

    public void Init(float angle)
    {

        _angle = angle - 90f;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        _movementDirection = transform.up;

        life = TickTimer.CreateFromSeconds(Runner, _destroyTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
        else
            transform.position += _bulletSpeed * Runner.DeltaTime * (Vector3)_movementDirection;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().Damage(_bulletDamage);
            Runner.Despawn(Object);
        }
    }
}

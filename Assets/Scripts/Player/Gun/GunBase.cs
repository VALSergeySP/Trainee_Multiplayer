using Fusion;
using UnityEngine;

public class GunBase : NetworkBehaviour
{
    private SpriteRenderer _gunSprite;

    [SerializeField] protected Transform _gunEndTransform;
    [SerializeField] protected GunSOBase _gunData;


    public float ShootDelay { get => _gunData.AttackDelay; }
    public int MaxBullets { get => _gunData.MaxBullets; }


    public virtual void Init()
    {
        _gunSprite = GetComponent<SpriteRenderer>();
        _gunSprite.sprite = _gunData.GunSprite;
    }

    public virtual void Shoot(float bulletDirection)
    {
        Runner.Spawn(_gunData.Bullet.gameObject, _gunEndTransform.position, Quaternion.identity, Object.InputAuthority, (runner, o) => 
        { 
            o.GetComponent<BulletProjectile>().Init(bulletDirection, _gunData.BulletDespawnTime); 
        });
    }
}

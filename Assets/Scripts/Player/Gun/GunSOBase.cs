using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Player/Gun/Gun Data")]
public class GunSOBase : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite _gunSprite;
    public Sprite GunSprite { get => _gunSprite; }

    [SerializeField] private BulletProjectile _bulletPrefab;
    public BulletProjectile Bullet { get => _bulletPrefab; }

    [SerializeField] private float _attackDistance;
    public float BulletDespawnTime { get => _attackDistance / _bulletPrefab.BulletSpeed; }
    public int BulletDamage { get => _bulletPrefab.Damage; }
    [SerializeField] private int _bulletsCount;
    public int BulletsCount { get => _bulletsCount; }
    [SerializeField] private float _attackDelay;
    public float AttackDelay { get => _attackDelay; }
    [SerializeField] private int _maxBullets;
    public int MaxBullets { get => _maxBullets; }
}

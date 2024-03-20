using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    [SerializeField] protected float _bulletSpreadAngle = 60f;
    public override void Shoot(float bulletDirection)
    {
        float step = _bulletSpreadAngle / _gunData.BulletsCount;
        float currentAngle = -_bulletSpreadAngle / 2;

        for (int i = 0; i < _gunData.BulletsCount; i++)
        {
            Runner.Spawn(_gunData.Bullet.gameObject, _gunEndTransform.position, Quaternion.identity, Object.InputAuthority, (runner, o) =>
            {
                o.GetComponent<BulletProjectile>().Init(bulletDirection + currentAngle, _gunData.BulletDespawnTime);
            });

            currentAngle += step;
        }
    }
}

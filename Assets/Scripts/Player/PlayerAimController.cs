using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAimController : NetworkBehaviour
{
    private NetworkObject _gun;
    [SerializeField] private GunBase _gunPrefab;
    [SerializeField] private Vector2 _gunSpawnOffset;
    private GunBase _gunScript;

    [SerializeField] private Transform _body;

    [Networked] private TickTimer _shootDelay { get; set; }

    private float _angle;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            _gun = Runner.Spawn(_gunPrefab.gameObject, transform.position + (Vector3)_gunSpawnOffset, transform.rotation, Object.InputAuthority);
            _gun.transform.parent = transform;
            _gunScript = _gun.GetComponent<GunBase>();
            _gunScript.Init();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data))
        {
            data.aimDirection.Normalize();

            if(data.aimDirection == Vector2.zero) { return; }

            RotateBody(data.aimDirection);
            RotateGun(data.aimDirection);
        }

        if (GetInput(out NetworkInputData shootData))
        {
            if (HasStateAuthority && _shootDelay.ExpiredOrNotRunning(Runner))
            {
                if (shootData.aimDirection != Vector2.zero)
                {
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        _shootDelay = TickTimer.CreateFromSeconds(Runner, _gunScript.ShootDelay);

        _gunScript.Shoot(_angle);
    }


    private void RotateBody(Vector2 aimDirection)
    {
        if (aimDirection.x < 0)
        {
            _body.rotation = Quaternion.Euler(_body.rotation.eulerAngles.x, 180f, _body.rotation.eulerAngles.z);
        }
        else
        {
            _body.rotation = Quaternion.Euler(_body.rotation.eulerAngles.x, 0f, _body.rotation.eulerAngles.z);
        }
    }

    private void RotateGun(Vector2 aimDirection)
    {
        if (_gun == null) return;

        _angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (aimDirection.x < 0)
        {
            _gun.transform.rotation = Quaternion.Euler(new Vector3(180, 0, _angle * -1));
        } else
        {
            _gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        }
    }
}

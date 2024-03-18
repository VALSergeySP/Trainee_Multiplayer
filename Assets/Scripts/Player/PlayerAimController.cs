using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAimController : NetworkBehaviour
{
    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _body;

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data))
        {
            data.aimDirection.Normalize();

            if(data.aimDirection == Vector2.zero) { return; }

            RotateBody(data.aimDirection);
            RotateGun(data.aimDirection);
        }
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
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        if (aimDirection.x < 0)
        {
            _gun.rotation = Quaternion.Euler(new Vector3(180, 0, angle * -1));
        } else
        {
            _gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}

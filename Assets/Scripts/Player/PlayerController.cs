using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _movementSpeed = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data))
        {
            data.moveDirection.Normalize();
            _rb.velocity = _movementSpeed * data.moveDirection;
        }
    }
}
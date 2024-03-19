using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _movementSpeed = 5f;

    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        if(Object.HasInputAuthority)
        {
            Debug.Log("Local!");
        } else
        {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            AudioListener audioListenerOnCamera = GetComponentInChildren<AudioListener>();
            audioListenerOnCamera.enabled = false;
        }
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
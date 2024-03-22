using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private Animator _animator;
    private int _velocityId = Animator.StringToHash("velocity");
    private int _isDeadId = Animator.StringToHash("Dead");


    private string SPECTATOR_TAG = "Spectator";
    private Rigidbody2D _rb;
    [SerializeField] private float _movementSpeed = 5f;

    private void OnPlayerDeath()
    {
        _animator.SetBool(_isDeadId, true);
        GetComponent<Collider2D>().enabled = false;
        gameObject.tag = SPECTATOR_TAG;

        CheckForAllPlayersDead();
    }

    private void CheckForAllPlayersDead()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        int deadPlayersCount = 0;
        foreach(var player in players)
        {
            if(player.tag == SPECTATOR_TAG) deadPlayersCount++;
        }

        if(deadPlayersCount >= Runner.SessionInfo.PlayerCount)
        {
            FindObjectOfType<GameStateManager>().OnAllPlayersDead();
        }
    }

    private void OnDisable()
    {
        GetComponent<PlayerHealthController>().OnPlayerDeathEvent -= OnPlayerDeath;
    }

    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        GetComponent<PlayerHealthController>().OnPlayerDeathEvent += OnPlayerDeath;

        if (Object.HasInputAuthority)
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
            _animator.SetFloat(_velocityId, _rb.velocity.magnitude);

            if (gameObject.CompareTag(SPECTATOR_TAG) && !_animator.GetBool(_isDeadId)) {
                _animator.SetBool(_isDeadId, true);
            }// Заменить
        }
    }
}
using Fusion;
using UnityEngine;
using static Fusion.NetworkBehaviour;

public class NetworkStateManager : NetworkBehaviour
{
    private int _currentStateId = 0;
    public int GameStateId { get => _currentStateId; }

    private GameStateManager _gameStateManager;

    public void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
    }

    public void ChangeGameState(int newState)
    {
        _currentStateId = newState;
        _gameStateManager.ChangeStateById(_currentStateId);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_ChangeGameState(int newState)
    {
        ChangeGameState(newState);
    }
}

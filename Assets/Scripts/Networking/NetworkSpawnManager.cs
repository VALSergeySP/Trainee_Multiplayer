using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NetworkSpawnManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private const string AIM_INPUT_NAME = "Aim";
    private const string MOVE_INPUT_NAME = "Move";

    UISessionsListHandler _sessionsListHandler;

    [SerializeField] private NetworkPrefabRef _playerUIInput;
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private PlayerInput _playerInput;
    private List<PlayerRef> _players = new List<PlayerRef>();
    public List<PlayerRef> Players { get => _players; }
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();


    private void Awake()
    {
        _sessionsListHandler = FindAnyObjectByType<UISessionsListHandler>(FindObjectsInactive.Include);
        _playerInput = FindAnyObjectByType<PlayerInput>(FindObjectsInactive.Include);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        _players.Add(player);

        if (runner.IsServer)
        {
            runner.Spawn(_playerUIInput, Vector3.zero, Quaternion.identity, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
        _players.Remove(player);
    }


    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (_playerInput == null)
        {
            InitializePlayer(runner);
        } else
        {
            var data = new NetworkInputData();

            if (_playerInput.actions[MOVE_INPUT_NAME].ReadValue<Vector2>().sqrMagnitude > 0)
            {
                data.moveDirection = _playerInput.actions[MOVE_INPUT_NAME].ReadValue<Vector2>();
            }
            if (_playerInput.actions[AIM_INPUT_NAME].ReadValue<Vector2>().sqrMagnitude > 0)
            {
                data.aimDirection = _playerInput.actions[AIM_INPUT_NAME].ReadValue<Vector2>();
            }

            input.Set(data);
        }
    }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        SceneManager.LoadScene(0);
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        runner.Shutdown();
    }

    #region useless
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnConnectedToServer(NetworkRunner runner) { }

    
    #endregion

    public void InitializePlayer(NetworkRunner runner)
    {
        if (_playerInput == null)
        {
            _playerInput = FindAnyObjectByType<PlayerInput>(FindObjectsInactive.Include);
        }
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) 
    {
        if (_sessionsListHandler == null) return;

        if(sessionList.Count != 0)
        {
            _sessionsListHandler.ClearList();

            foreach (var sessionInfo in sessionList)
            {
                _sessionsListHandler.AddToList(sessionInfo);
            }
        }
    }
}

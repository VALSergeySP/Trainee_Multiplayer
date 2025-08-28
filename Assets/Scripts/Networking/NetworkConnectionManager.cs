using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class NetworkConnectionManager : NetworkBehaviour, INetworkRunnerCallbacks
{
    private const string MAIN_MENU_SCENE = "MainMenuScene";

    private readonly string _lobbyName = "CustomLobby";

    [SerializeField] private NetworkRunner _runnerPrefab;
    private NetworkRunner _networkRunner;

    private void Awake()
    {
        NetworkRunner runnerInScene = FindObjectOfType<NetworkRunner>();

        if (runnerInScene != null)
        {
            _networkRunner = runnerInScene;
        }
    }

    private void Start()
    {
        if (_networkRunner == null)
        {
            InitializeRunner(false);
        }
    }

    private void InitializeRunner(bool connectToLobby)
    {
        _networkRunner = Instantiate(_runnerPrefab);
        _networkRunner.name = "Network runner";
        _networkRunner.ProvideInput = true;

        if (SceneManager.GetActiveScene().name != MAIN_MENU_SCENE)
        {
            var clientTask = StartGame(_networkRunner, GameMode.AutoHostOrClient, "Test", NetAddress.Any(), SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath($"scenes/GameScene")), _networkRunner.GetComponent<NetworkSceneManagerDefault>());
        }

        if(connectToLobby)
        {
            OnJoinLobby();
        }
    }

    private async Task StartGame(NetworkRunner runner, GameMode mode, string sessionName, NetAddress address, SceneRef scene, NetworkSceneManagerDefault sceneManager)
    {
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = _lobbyName,
            SceneManager = sceneManager
        });
    }

    #region Useless
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    #endregion

    public void OnJoinLobby()
    {
        if (_networkRunner != null)
        {
            var clientTask = JoinLobby();
        } else
        {
            InitializeRunner(true);
        }
    }

    private async Task JoinLobby()
    {
        var result = await _networkRunner.JoinSessionLobby(SessionLobby.Custom, _lobbyName);

        if (!result.Ok)
        {
            Debug.LogError("Cannot connect to the lobby!");
        }
        else
        {
            Debug.Log("Connected to lobby!");
        }
    }

    public void CreateGame(string sessionName, string sceneName)
    {
        var clientTask = StartGame(_networkRunner, GameMode.Host, sessionName, NetAddress.Any(), SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}")), _networkRunner.GetComponent<NetworkSceneManagerDefault>());
    }

    public void JoinGame(SessionInfo sessionInfo)
    {
        var clientTask = StartGame(_networkRunner, GameMode.Client, sessionInfo.Name, NetAddress.Any(), SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), _networkRunner.GetComponent<NetworkSceneManagerDefault>());
    }
 }
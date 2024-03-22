using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : NetworkBehaviour
{
    private NetworkStateManager _networkStateManager;
    public NetworkStateManager NetworkStateManagerInstance { get => _networkStateManager; }

    [SerializeField] private WaveSO[] _waves;
    [SerializeField] private float _waitTime = 30f;
    [SerializeField] private Vector2 _mapSizes;
    public float WaitTime { get => _waitTime; }
    private int _currentWave;

    public bool IsLoading { get; private set; }

    public GameStateMachine GameManagerStateMachine { get; private set; }
    public GameLoadingState LoadingState { get; private set; }
    public GameStartState StartState { get; private set; }
    public GameWaveState WaveState { get; private set; }
    public GameWaitState WaitState { get; private set; }
    public GameEndState EndState { get; private set; }

    private void TestFunction() // Test
    {
        IsLoading = false;
    }

    public void SendRpcStateId(int id)
    {
        if (Runner.IsServer)
        {
            NetworkStateManagerInstance.RPC_ChangeGameState(id);
        }
    }

    public void OnAllPlayersDead()
    {
        if (Runner.IsServer)
        {
            GameManagerStateMachine.ChangeState(EndState);
        }
    }

    public bool CanSpawnObjects()
    {
        return Runner.IsServer;
    }

    public void ChangeStateById(int id)
    {
        if(Runner.IsServer) { return; }
        switch(id)
        {
            case 0:
                GameManagerStateMachine.ChangeState(LoadingState);
                break;
            case 1:
                GameManagerStateMachine.ChangeState(StartState);
                break;
            case 2:
                GameManagerStateMachine.ChangeState(WaveState);
                break;
            case 3:
                GameManagerStateMachine.ChangeState(WaitState);
                break;
            case 4:
                GameManagerStateMachine.ChangeState(EndState);
                break;
        }
    }

    private void Awake()
    {
        IsLoading = true;
        _currentWave = 0;
        GameManagerStateMachine = new GameStateMachine();
        

        LoadingState = new GameLoadingState(this, GameManagerStateMachine);
        StartState = new GameStartState(this, GameManagerStateMachine);
        WaveState = new GameWaveState(this, GameManagerStateMachine);
        WaitState = new GameWaitState(this, GameManagerStateMachine);
        EndState = new GameEndState(this, GameManagerStateMachine);
    }

    private void Start()
    {
        GameManagerStateMachine.Initialize(LoadingState);
        _networkStateManager = GetComponent<NetworkStateManager>();

        Invoke(nameof(TestFunction), 3f); // Test
    }

    private void Update()
    {
        GameManagerStateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        GameManagerStateMachine.CurrentState.PhysicsUpdate();
    }

    public WaveSO GetNextWave()
    {
        if (_currentWave < _waves.Length)
        {
            _currentWave++;
            return _waves[_currentWave - 1];
        }
        else
        {
            return null;
        }
    }

    public bool CheckIsGameEnd()
    {
        return _currentWave >= _waves.Length;
    }

    public void SpawnNewItem(CollectibleItemBase item)
    {
        if (Runner.IsServer)
        {
            NetworkObject obj = Runner.Spawn(item.gameObject, GetRandomSpawnPosition(), Quaternion.identity, null);
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-_mapSizes.x, _mapSizes.x);
        float y = Random.Range(-_mapSizes.y, _mapSizes.y);

        return new Vector2(x, y);
    }
}

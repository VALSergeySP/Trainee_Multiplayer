using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public bool IsLoading { get; private set; }

    public GameStateMachine GameManagerStateMachine { get; private set; }
    public GameLoadingState LoadingState { get; private set; }
    public GameStartState StartState { get; private set; }
    public GameWaveState WaveState { get; private set; }
    public GameWaitState WaitState { get; private set; }
    public GameEndState EndState { get; private set; }

    public void StartGameButton()
    {
        GameManagerStateMachine.ChangeState(WaveState);
    }

    private void TestFunction() // Test
    {
        IsLoading = false;
    }

    private void Awake()
    {
        IsLoading = true;
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


}

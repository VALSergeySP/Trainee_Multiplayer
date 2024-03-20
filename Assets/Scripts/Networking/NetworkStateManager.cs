using Fusion;
using UnityEngine;

public class NetworkStateManager : NetworkBehaviour
{
    private ChangeDetector _changeDetector;

    [Networked] public int GameStateId { get; set; }

    private GameStateManager _gameStateManager;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        _gameStateManager = FindObjectOfType<GameStateManager>();
    }

    public void ChangeGameState(int newState)
    {
        GameStateId = newState;
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(GameStateId):
                    _gameStateManager.ChangeStateById(GameStateId);
                    break;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Wave", menuName = "Game Logic/Waves/Wave")]
public class WaveSO : ScriptableObject
{
    [Serializable]
    public struct EnemyToSpawn
    {
        public Enemy enemy;
        public int count;
    }

    // Add pickups later

    [SerializeField] protected float _waveDurationTime = 1f;
    [SerializeField] protected EnemyToSpawn[] _enemies;
    private float _timer;
    protected GameStateManager _gameManager;

    public virtual void Init(GameStateManager gameManager)
    {
        Debug.Log("Base wave!");
        _timer = 0;
        _gameManager = gameManager;
    }

    public virtual void DoFrameUpdateLogic()
    {
        if (_timer > _waveDurationTime)
        {
            _gameManager.GameManagerStateMachine.ChangeState(_gameManager.WaitState);
        }

        _timer += Time.deltaTime;
    }
}

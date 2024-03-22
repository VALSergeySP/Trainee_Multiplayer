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

    [Serializable]
    public struct ItemToSpawn
    {
        public CollectibleItemBase item;
        public int count;
    }

    // Add pickups later
    [SerializeField] protected string _waveName = "Base";
    public string WaveName { get => _waveName; }
    [SerializeField] protected float _waveDurationTime = 1f;
    public float WaveDurationTime { get => _waveDurationTime; }
    [SerializeField] protected EnemyToSpawn[] _enemies;
    [SerializeField] protected ItemToSpawn[] _items;

    private float _timer;
    protected GameStateManager _gameManager;

    public virtual void Init(GameStateManager gameManager)
    {
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

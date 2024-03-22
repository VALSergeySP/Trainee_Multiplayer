using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Linear Wave", menuName = "Game Logic/Waves/Linear Wave")]
public class WaveWithLinearSpawn : WaveSO
{
    private List<float> _timeToSpawnEachEnemy = new();
    private List<float> _timersForEachEnemy = new();

    private List<float> _timeToSpawnEachItem = new();
    private List<float> _timersForEachItem = new();

    public override void Init(GameStateManager gameManager)
    {
        base.Init(gameManager);

        float time;

        for (int i = 0; i < _enemies.Length; i++)
        {
            time = _waveDurationTime / (_enemies[i].count);
            _timeToSpawnEachEnemy.Add(time);
            _timersForEachEnemy.Add(0f);
        }

        for (int i = 0; i < _items.Length; i++)
        {
            time = _waveDurationTime / (_items[i].count);
            _timeToSpawnEachItem.Add(time);
            _timersForEachItem.Add(0f);
        }
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_timersForEachEnemy[i] > _timeToSpawnEachEnemy[i])
            {
                _timersForEachEnemy[i] = 0f;
                SpawnEnemyOfType(_enemies[i].enemy);
            }

            _timersForEachEnemy[i] += Time.deltaTime;
        }

        for (int i = 0; i < _items.Length; i++)
        {
            if (_timersForEachItem[i] > _timeToSpawnEachItem[i])
            {
                _timersForEachItem[i] = 0f;
                SpawnItemOfType(_items[i].item);
            }

            _timersForEachItem[i] += Time.deltaTime;
        }
    }

    private void SpawnEnemyOfType(Enemy enemy)
    {
        EnemiesSpawner.Instance.SpawnNewEnemy(enemy); // Перенести в гейм менеджера
    }

    private void SpawnItemOfType(CollectibleItemBase item)
    {
        _gameManager.SpawnNewItem(item);
    }
}

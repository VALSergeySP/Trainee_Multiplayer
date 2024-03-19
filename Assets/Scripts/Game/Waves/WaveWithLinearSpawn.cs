using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Linear Wave", menuName = "Game Logic/Waves/Linear Wave")]
public class WaveWithLinearSpawn : WaveSO
{
    private List<float> _timeToSpawnEachEnemy = new();
    private List<float> _timersForEachEnemy = new();

    public override void Init(GameStateManager gameManager)
    {
        base.Init(gameManager);
        Debug.Log("Linear wave!");

        float time;

        for (int i = 0; i < _enemies.Length; i++)
        {
            time = _waveDurationTime / (_enemies[i].count);
            _timeToSpawnEachEnemy.Add(time);
            _timersForEachEnemy.Add(0f);
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
    }

    private void SpawnEnemyOfType(Enemy enemy)
    {
        Debug.Log("Spawned enemy!");

        EnemiesSpawner.Instance.SpawnNewEnemy(enemy);
    }
}

using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : NetworkBehaviour
{

    [SerializeField] private GameObject _spawnerPrefab;
    [SerializeField] private int _spawnersOnMap = 5;
    [SerializeField] private Vector2 _mapSizes;
    private List<Transform> _spawnersTransfroms = new();

    public static EnemiesSpawner Instance { get; private set; } 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        for (int i = 0; i < _spawnersOnMap; i++)
        {
            Vector2 randomPostion = GetRandomSpawnPosition();
            NetworkObject obj = Runner.Spawn(_spawnerPrefab, randomPostion, Quaternion.identity, Object.InputAuthority);
            obj.transform.parent = transform;
            _spawnersTransfroms.Add(obj.transform);
        }
    }

    public void SpawnNewEnemy(Enemy enemy)
    {
        if (Runner.IsServer)
        {
            int spawnerNum = Random.Range(0, _spawnersTransfroms.Count);

            NetworkObject obj = Runner.Spawn(enemy.gameObject, _spawnersTransfroms[spawnerNum].transform.position, Quaternion.identity, Object.InputAuthority);

            obj.transform.parent = transform;
        }
    }

    public void SpawnBulletInNetwork(BulletProjectile obj, Vector2 pos, float angle, float despawnTime)
    {
        if (Runner.IsServer)
        {
            Runner.Spawn(obj.gameObject, pos, Quaternion.identity, Object.InputAuthority, (runner, o) =>
            {
                o.GetComponent<BulletProjectile>().Init(angle, despawnTime);
            });
        }
    }
    public void SpawnMelleeInNetwork(EnemyMeleeProjectile obj, Vector2 pos, float despawnTime)
    {
        if (Runner.IsServer)
        {
            Runner.Spawn(obj.gameObject, pos, Quaternion.identity, Object.InputAuthority, (runner, o) =>
            {
                o.GetComponent<EnemyMeleeProjectile>().Init(despawnTime);
            });
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-_mapSizes.x, _mapSizes.x);
        float y = Random.Range(-_mapSizes.y, _mapSizes.y);

        return new Vector2(x, y);
    }
}

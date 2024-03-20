using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerSpawner : NetworkBehaviour
{
    private int _variantNum = 0;
    [SerializeField] private Button _startButton; 

    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private NetworkObject[] _playerGunPrefabs;
    [SerializeField] private Sprite[] _playerSprites;
    private List<NetworkObject> _gunsList = new();
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public override void Spawned()
    {
        if(Runner.IsClient)
        {
            _startButton.gameObject.SetActive(false);
        }
    }

    public void OnVariant(int num)
    {
        _variantNum = num;
    }

    public void OnStartButton()
    {
        SpawnPlayers();
        GameStateManager gameManager = FindObjectOfType<GameStateManager>();
        gameManager.GameManagerStateMachine.ChangeState(gameManager.WaveState);
    }

    private void SpawnPlayers()
    {
        Debug.Log(Runner.ToString());
        if (Runner.IsServer)
        {
            NetworkSpawnManager networkSpawnManager = FindObjectOfType<NetworkSpawnManager>();

            for (int i = 0; i < _playerGunPrefabs.Length; i++)
            {
                _gunsList.Add(_playerGunPrefabs[i]);
            }

            foreach (var player in networkSpawnManager.Players)
            {
                int num = Random.Range(0, _gunsList.Count);

                NetworkObject gun = _gunsList[num];
                _gunsList.RemoveAt(num);

                // Create a unique position for the player
                Vector3 spawnPosition = new Vector3((player.RawEncoded % Runner.Config.Simulation.PlayerCount) * 3, 1, 0);

                NetworkObject networkPlayerObject = Runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
                NetworkObject newPlayerGunObject = Runner.Spawn(gun, spawnPosition, Quaternion.identity, player);
                networkPlayerObject.GetComponent<PlayerAimController>().Init(newPlayerGunObject);

                if (_playerSprites.Length > _variantNum)
                {
                    networkPlayerObject.GetComponent<PlayerController>().Init(_playerSprites[_variantNum]);
                }

                networkPlayerObject.transform.parent = networkPlayerObject.transform;

                // Keep track of the player avatars for easy access
                _spawnedCharacters.Add(player, networkPlayerObject);
            }
        }
    }
}

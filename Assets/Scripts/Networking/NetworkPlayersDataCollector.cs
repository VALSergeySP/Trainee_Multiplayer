using Fusion;
using UnityEngine;

public class NetworkPlayersDataCollector : NetworkBehaviour
{
    private NetworkUIInput[] _rpcSenders;

    private int[] _playersKills = { 0, 0 };
    private int[] _playersDamage = { 0, 0 };
    private int[] _playersVariants = { 0, 0 };
    private bool[] _playersStatus = { true, true };

    public void AddKillToPlayer(int player)
    {
        Debug.Log($"Player {player} killed enemy!");
        _playersKills[player - 1]++;

        _rpcSenders = FindObjectsOfType<NetworkUIInput>();

        foreach (var input in _rpcSenders)
        {
            input.OnKillsChanged(_playersKills[player - 1], player);
        }
    }

    public void AddDamageToPlayer(int damage, int player)
    {
        Debug.Log($"Player {player} dealt {damage} damage!");
        _playersDamage[player - 1] += damage;
    }


    public void SetPlayerVariant(int variant, int player) 
    {
        _playersVariants[player - 1] = variant;
    }


    private void SetPlayersStatus()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].gameObject.CompareTag("Player"))
            {
                _playersStatus[i] = false;
            }
        }
    }

    public (int[], int[], int[], bool[]) GetPlayersData()
    {
        SetPlayersStatus();
        return (_playersVariants, _playersKills, _playersDamage, _playersStatus);
    }
}

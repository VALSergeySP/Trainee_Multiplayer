using Fusion;
using UnityEngine;

public class NetworkUIInput : NetworkBehaviour
{
    public void OnVariant(int num)
    {
        if (Object.HasInputAuthority)
        {
            RPC_SendPlayerChangedSkin(num);
        }
    }

    public void OnEndGame()
    {
        if(Runner.IsServer)
        {
            NetworkPlayersDataCollector _gameDataManager = FindObjectOfType<NetworkPlayersDataCollector>();

            var data = _gameDataManager.GetPlayersData();

            RPC_RelayEndGameInfo(data.Item1, data.Item2, data.Item3, data.Item4);

            EnemiesSpawner.Instance.DeleteAllEnemies();
        }
    }

    public void OnKillsChanged(int kills, int playerId)
    {
        if (Runner.IsServer)
        {
            RPC_RelayPlayerKills(kills, playerId);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendPlayerChangedSkin(int newVariant, RpcInfo info = default)
    {
        FindObjectOfType<NetworkPlayerSpawner>().SetVariant(newVariant, info.Source.PlayerId - 1);
        RPC_RelayPlayerChangedSkin(newVariant, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayPlayerChangedSkin(int newVariant, PlayerRef messageSource)
    {
        UIManager.Instance.SkinMenu.SetCurrentVariant(newVariant, messageSource.PlayerId);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayPlayerKills(int killsCount, int playerId)
    {
        if (Object.HasInputAuthority)
        {
            if (playerId == Object.InputAuthority.PlayerId)
            {
                UIManager.Instance.KillsCountManager.SetKillsCount(killsCount);
            }
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayEndGameInfo(int[] variants, int[] kills, int[] damage, bool[] status)
    {
        UIEndGameInfoManager _gameInfoManager = FindObjectOfType<UIEndGameInfoManager>();

        _gameInfoManager.SetEndGameInfo(Runner.SessionInfo.PlayerCount, variants, kills, damage, status, Runner.IsServer);
    }
}

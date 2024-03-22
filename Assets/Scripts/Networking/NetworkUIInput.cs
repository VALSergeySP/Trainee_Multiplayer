using Fusion;

public class NetworkUIInput : NetworkBehaviour
{
    private UIManager _uiManager;

    public void InitBullets(int bullets, int playerId)
    {
        if (Runner.IsServer)
        {
            RPC_RelayPlayerBullets(bullets, playerId, true);
        }
    }

    public void SetBullets(int bullets, int playerId)
    {
        if (Runner.IsServer)
        {
            RPC_RelayPlayerBullets(bullets, playerId, false);
        }
    }

    public override void Spawned()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

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
        _uiManager.SkinMenu.SetCurrentVariant(newVariant, messageSource.PlayerId);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayPlayerKills(int killsCount, int playerId)
    {
        if (Object.HasInputAuthority)
        {
            if (playerId == Object.InputAuthority.PlayerId)
            {
                _uiManager.KillsCountManager.SetKillsCount(killsCount);
            }
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayPlayerBullets(int bullets, int playerId, bool init = false)
    {
        if (Object.HasInputAuthority)
        {
            if (playerId == Object.InputAuthority.PlayerId)
            {
                if (init)
                {
                    _uiManager.BulletsCountManager.Init(bullets);
                } else
                {
                    _uiManager.BulletsCountManager.SetBulletsCount(bullets);
                }
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

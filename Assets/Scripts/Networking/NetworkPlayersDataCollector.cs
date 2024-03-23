using Fusion;

public class NetworkPlayersDataCollector : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    private bool _connected = false;

    private NetworkUIInput[] _rpcSenders;

    private int[] _playersKills = { 0, 0 };
    private int[] _playersDamage = { 0, 0 };
    private int[] _playersVariants = { 0, 0 };
    private bool[] _playersStatus = { true, true };

    private void FindRpcSenders()
    {
        _rpcSenders = FindObjectsOfType<NetworkUIInput>();
    }

    public void AddKillToPlayer(int player)
    {
        _playersKills[player - 1]++;

        if(!_connected)
        {
            FindRpcSenders();
        }

        foreach (var input in _rpcSenders)
        {
            input.OnKillsChanged(_playersKills[player - 1], player);
        }
    }

    public void AddDamageToPlayer(int damage, int player)
    {
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
            if (!players[i].gameObject.CompareTag(PLAYER_TAG))
            {
                int playerId = players[i].Object.InputAuthority.PlayerId;
                _playersStatus[playerId - 1] = false;
            }
        }
    }

    public (int[], int[], int[], bool[]) GetPlayersData()
    {
        SetPlayersStatus();
        return (_playersVariants, _playersKills, _playersDamage, _playersStatus);
    }
}

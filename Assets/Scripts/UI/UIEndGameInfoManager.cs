using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGameInfoManager : MonoBehaviour
{
    private int _playersCount = 1;
    [SerializeField] private GameObject _secondPlayerLine;

    [SerializeField] private Sprite[] _variantsImages;
    [SerializeField] private Image[] _playersImages;
    [SerializeField] private TMP_Text[] _playersAutorityText;
    
    [SerializeField] private TMP_Text[] _playersKills;
    [SerializeField] private TMP_Text[] _playersDamage;

    [SerializeField] private Sprite[] _statusImages;
    [SerializeField] private Image[] _playersStatus;

    public void SetEndGameInfo(int playersCount, int[] playersVariantsNum, int[] playersKills, int[] playersDamage, bool[] isPlayersAlive, bool isHost) 
    {
        _playersCount = playersCount;

        if (_playersCount == 1)
        {
            _secondPlayerLine.SetActive(false);
        }

        if(isHost)
        {
            _playersAutorityText[0].text = "You";
            _playersAutorityText[1].text = "";
        } else
        {
            _playersAutorityText[0].text = "";
            _playersAutorityText[1].text = "You";
        }

        for (int i = 0; i < _playersCount; i++)
        {
            _playersImages[i].sprite = _variantsImages[playersVariantsNum[i]];
            _playersKills[i].text = playersKills[i].ToString();
            _playersDamage[i].text = playersDamage[i].ToString();
            if (isPlayersAlive[i])
            {
                _playersStatus[i].sprite = _statusImages[0];
            } else
            {
                _playersStatus[i].sprite = _statusImages[1];
            }
        }
    }
}

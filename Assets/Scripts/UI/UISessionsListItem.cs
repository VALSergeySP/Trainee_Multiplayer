using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System;

public class UISessionsListItem : MonoBehaviour
{
    [SerializeField] TMP_Text _sessionName;
    [SerializeField] Button _connectButton;

    SessionInfo _sessionInfo;

    public event Action<SessionInfo> OnJoinSession;

    public void SetInformation(SessionInfo sessionInfo)
    {
        _sessionInfo = sessionInfo;

        _sessionName.text = _sessionInfo.Name;

        if(_sessionInfo.PlayerCount >= 2)
        {
            _connectButton.interactable = false;
        }
    }

    public void OnClick()
    {
        OnJoinSession?.Invoke(_sessionInfo);
    }
}

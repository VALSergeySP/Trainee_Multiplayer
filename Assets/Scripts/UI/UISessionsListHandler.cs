using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISessionsListHandler : MonoBehaviour
{
    [SerializeField] private GameObject _sessionsListItemPrefab;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

    private void Awake()
    {
        ClearList();
    }

    public void ClearList()
    {
        foreach (Transform child in _verticalLayoutGroup.transform)
        {
            UISessionsListItem childScript = child.GetComponent<UISessionsListItem>();
            if (childScript != null)
            {
                childScript.OnJoinSession -= OnJoinSession;
            }

            Destroy(child.gameObject);
        }
    }

    public void AddToList(SessionInfo sessionInfo)
    {
        UISessionsListItem newItem = Instantiate(_sessionsListItemPrefab, _verticalLayoutGroup.transform).GetComponent<UISessionsListItem>();
        newItem.SetInformation(sessionInfo);

        newItem.OnJoinSession += OnJoinSession;
    }

    private void OnJoinSession(SessionInfo sessionInfo)
    {
        NetworkConnectionManager networkManager = FindObjectOfType<NetworkConnectionManager>();

        networkManager.JoinGame(sessionInfo);
    }
}

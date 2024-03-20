using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _createCanvas;
    [SerializeField] private GameObject _joinCanvas;

    [SerializeField] private TMP_InputField _sessionNameInput;

    [SerializeField] private string _sceneName = "GameScene";

    private NetworkConnectionManager _networkManager;

    private void Start()
    {
        _mainCanvas.SetActive(true);
        _createCanvas.SetActive(false);
        _joinCanvas.SetActive(false);

        _networkManager = FindAnyObjectByType<NetworkConnectionManager>();
        Invoke(nameof(StartLobby), 1f);
    }


    private void StartLobby()
    {
        _networkManager.OnJoinLobby();
    }

    public void OnCreateNewSessionButton()
    {
        _networkManager.CreateGame(_sessionNameInput.text, _sceneName);
    }


    public void OnBackButton()
    {
        _createCanvas.SetActive(false);
        _joinCanvas.SetActive(false);
        _mainCanvas.SetActive(true);
    }

    public void OnJoinButton()
    {
        _createCanvas.SetActive(false);
        _mainCanvas.SetActive(false);
        _joinCanvas.SetActive(true);
    }

    public void OnCreateButton()
    {
        _joinCanvas.SetActive(false);
        _mainCanvas.SetActive(false);
        _createCanvas.SetActive(true);
    }
}

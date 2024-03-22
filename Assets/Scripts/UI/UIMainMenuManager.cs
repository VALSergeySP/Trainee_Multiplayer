using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _createCanvas;
    [SerializeField] private GameObject _joinCanvas;
    [SerializeField] private GameObject _loadingCanvas;

    [SerializeField] private TMP_InputField _sessionNameInput;
    [SerializeField] private TMP_Text _errorTextMessage;

    [SerializeField] private string _sceneName = "GameScene";

    private NetworkConnectionManager _networkManager;

    private void Start()
    {
        _loadingCanvas.SetActive(true);
        _mainCanvas.SetActive(false);
        _createCanvas.SetActive(false);
        _joinCanvas.SetActive(false);

        _networkManager = FindAnyObjectByType<NetworkConnectionManager>();
        if (_networkManager != null)
        {
            Invoke(nameof(StartLobby), 0.1f);
            Invoke(nameof(OnLoading), 3f);
        }
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    private void StartLobby()
    {
        _networkManager.OnJoinLobby();
    }

    private void OnLoading()
    {
        _loadingCanvas.SetActive(false);
        _mainCanvas.SetActive(true);
    }

    public void OnCreateNewSessionButton()
    {
        if (_sessionNameInput.text.Length > 0)
        {
            _loadingCanvas.SetActive(true);
            _networkManager.CreateGame(_sessionNameInput.text, _sceneName);
        } else
        {
            _errorTextMessage.text = "Enter name!";
        }
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

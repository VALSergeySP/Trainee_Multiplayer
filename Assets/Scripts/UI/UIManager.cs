using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager() { }

    public static UIManager Instance { get; private set; }



    [SerializeField] private Canvas _loadingCanvas;
    public Canvas LoadingCanvas { get => _loadingCanvas; }

    [SerializeField] private Canvas _playerControllCanvas;
    public Canvas PlayerControllCanvas { get => _playerControllCanvas; }

    [SerializeField] private Canvas _gameUICanvas;
    public Canvas GameUICanvas { get => _gameUICanvas; }

    [SerializeField] private Canvas _gameStartCanvas;
    public Canvas GameStartCanvas { get => _gameStartCanvas; }

    [SerializeField] private Canvas _endGameCanvas;
    public Canvas EndGameCanvas { get => _endGameCanvas; }

    void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(this); }
    }
}

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

    private UITimer _timer;
    public UITimer Timer { get => _timer; }

    private UISkinChooseMenu _skinChooseMenu;
    public UISkinChooseMenu SkinMenu {get =>_skinChooseMenu; }

    private UIKillsCountManager _killsCountMenu;
    public UIKillsCountManager KillsCountManager { get => _killsCountMenu; }

    void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(this); }

        _timer = GetComponent<UITimer>();
        _skinChooseMenu = GetComponent<UISkinChooseMenu>();
        _killsCountMenu = GetComponent<UIKillsCountManager>();
    }
}

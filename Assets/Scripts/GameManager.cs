using System;
using System.IO;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  GameManager : MonoBehaviour
{
    private InterfaceController _interfaceController;
    private GameTimer _gameTimer;
    private GameOptions _gameOptions;
    private bool _gamePaused;

    private void Awake()
    {
        _gameTimer = GetComponent<GameTimer>();
        _interfaceController = GetComponent<InterfaceController>();
        
        InitializeGameDifficulty("");
        _gameTimer.InitialDuration = _gameOptions.gameTimerDuration;
        _gameTimer.ResetTimer();
        _gameTimer.HandleTimerExpiration = EndGame;
        _gameTimer.TimerText = _interfaceController.gameTimeText;
        
        _interfaceController.playButton.onClick.AddListener(PlayGame);
        _interfaceController.restartGameButton.onClick.AddListener(RestartGame);
        _interfaceController.resumeGameButton.onClick.AddListener(PlayGame);
        _interfaceController.quitGameButton.onClick.AddListener(RestartGame);
    }

    private void Start()
    {
        Time.timeScale = 0;
        _gamePaused = true; 
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (!_gamePaused) PauseGame();
        else PlayGame();
    }

    private void PlayGame()
    {
        // Read article for understanding: https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
        Time.timeScale = 1;
        _gamePaused = false;
        _interfaceController.mainMenuScreen.SetActive(false);
        _interfaceController.pauseMenuScreen.SetActive(false);
        _interfaceController.gameOverlayScreen.SetActive(true);
    }

    private void PauseGame()
    {
        _interfaceController.pauseMenuScreen.SetActive(true);
        Time.timeScale = 0;
        _gamePaused = true; 
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Easy");
    }

    private void EndGame()
    {
        _interfaceController.gameOverlayScreen.SetActive(false);
        _interfaceController.gameOverScreen.SetActive(true);
    }

    private void InitializeGameDifficulty(string difficulty)
    {
        difficulty = "EasyMode.json";
        string path = Application.dataPath + "/Data/" + difficulty;
        string contents = File.ReadAllText(path);
        GameOptions gameOptions = JsonUtility.FromJson<GameOptions>(contents);
        _gameOptions = gameOptions;
    }
}
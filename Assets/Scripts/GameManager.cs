using System.IO;
using Model;
using UnityEngine;

public class  GameManager : MonoBehaviour
{
    private GameTimer _gameTimer;
    private GameOptions _gameOptions;

    private void Awake()
    {
        _gameTimer = GetComponent<GameTimer>();
        
        InitializeGameDifficulty("");
        _gameTimer.InitialDuration = _gameOptions.gameTimerDuration;
        _gameTimer.ResetTimer();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        
        if (_gameTimer.TimerIsRunning)
        {
            _gameTimer.Pause();
        } else _gameTimer.Play();
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
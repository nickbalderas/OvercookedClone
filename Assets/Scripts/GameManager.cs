using System.IO;
using Model;
using UnityEngine;

public class  GameManager : MonoBehaviour
{
    private GameTimer _gameTimer;
    private GameOptions _gameOptions;
    private bool _gamePaused;

    private void Awake()
    {
        _gameTimer = GetComponent<GameTimer>();
        
        PauseGame();
        InitializeGameDifficulty("");
        _gameTimer.InitialDuration = _gameOptions.gameTimerDuration;
        _gameTimer.ResetTimer();
        _gameTimer.HandleTimerExpiration = () => Debug.Log("Game Over!");

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
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _gamePaused = true;
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
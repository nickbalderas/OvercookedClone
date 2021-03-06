using System.Collections;
using System.IO;
using Model;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class  GameManager : MonoBehaviour
{
    private InterfaceController _interfaceController;
    private GameTimer _gameTimer;
    private GameOptions _gameOptions;
    public bool gamePaused;
    private bool _gameStarted;

    private void Awake()
    {
        Time.timeScale = 0;
        gamePaused = true;
        
        _gameTimer = GetComponent<GameTimer>();
        _interfaceController = GetComponent<InterfaceController>();
        
        InitializeGameDifficulty("EasyMode.json");
        _gameTimer.InitialDuration = _gameOptions.gameTimerDuration;
        _gameTimer.ResetTimer();
        _gameTimer.HandleTimerExpiration = EndGame;
        _gameTimer.TimerText = _interfaceController.gameTimeText;
        
        _interfaceController.playButton.onClick.AddListener(PlayGame);
        _interfaceController.restartGameButton.onClick.AddListener(RestartGame);
        _interfaceController.resumeGameButton.onClick.AddListener(PlayGame);
        _interfaceController.quitGameButton.onClick.AddListener(RestartGame);
    }

    private void Update()
    {
        if (!_gameStarted) return;
        
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (!gamePaused) PauseGame();
        else PlayGame();
    }

    private void PlayGame()
    {
        _gameStarted = true;
        // Read article for understanding: https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
        Time.timeScale = 1;
        gamePaused = false;
        _interfaceController.pauseMenuScreen.SetActive(false);
        _interfaceController.mainMenuScreen.SetActive(false);
        _interfaceController.pauseMenuScreen.SetActive(false);
        _interfaceController.gameOverlayScreen.SetActive(true);
    }

    private void PauseGame()
    {
        _interfaceController.pauseMenuScreen.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true; 
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Easy");
    }

    private void EndGame()
    {
        _interfaceController.gameOverlayScreen.SetActive(false);
        _interfaceController.gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true; 
    }

    private void InitializeGameDifficulty(string difficulty)
    {
        string path = Application.dataPath + "/StreamingAssets/" + difficulty;
        
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine("WebGLJsonRead", path);
        }
        else
        {
            string contents = File.ReadAllText(path);
            GameOptions gameOptions = JsonUtility.FromJson<GameOptions>(contents);
            _gameOptions = gameOptions; 
        }
    }

    IEnumerator WebGLJsonRead(string path)
    {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        _gameOptions = JsonUtility.FromJson<GameOptions>(www.downloadHandler.text);
    }
}
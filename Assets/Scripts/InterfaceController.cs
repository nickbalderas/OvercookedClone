using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InterfaceController : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public Button playButton;

    public GameObject gameOverlayScreen;
    public TextMeshProUGUI gameTimeText;

    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public Button restartGameButton;

    public GameObject pauseMenuScreen;
    public Button quitGameButton;
    public Button resumeGameButton;
    
    private void Awake()
    {
        mainMenuScreen.SetActive(true);
    }
}
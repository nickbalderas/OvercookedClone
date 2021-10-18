using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int _score;
    private InterfaceController _interfaceController;

    private void Awake()
    {
        _interfaceController = GameObject.Find("Game Manager").GetComponent<InterfaceController>();
    }

    private void Start()
    {
        _interfaceController.scoreDisplay.text = "0";
    }

    public void UpdateScore(int value)
    {
        _score += value;
        if (_score < 0) _score = 0;
        _interfaceController.scoreDisplay.text = $"{_score}";
        _interfaceController.finalScoreText.text = $"{"Final Score: " + _score}";
    }
}
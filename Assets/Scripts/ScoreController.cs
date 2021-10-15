using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int _score;

    public void UpdateScore(int value)
    {
        _score += value;
        if (_score < 0) _score = 0;
        Debug.Log("Current Score: " + _score);
    }
}
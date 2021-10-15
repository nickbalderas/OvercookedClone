using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float InitialDuration { get; set; }
    public float TimeRemaining { get; set; }
    
    private Text TimerText { get; set; }

    public Action HandleTimerExpiration;

    private void Update()
    {
        if (Time.timeScale == 0 || TimeRemaining == 0) return;

        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            DisplayTime(TimeRemaining);
        }else TimeExpired();
    }

    private void TimeExpired()
    {
        TimeRemaining = 0;
        HandleTimerExpiration();
    }

    public void ResetTimer()
    {
        TimeRemaining = InitialDuration;
    }

    private static void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // TimerText.text = $"{minutes:00}:{seconds:00}";
        // Debug.Log($"{minutes:00}:{seconds:00}");
    }
}
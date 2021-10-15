using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float InitialDuration { get; set; }
    public float TimeRemaining { get; set; }
    public bool TimerIsRunning { get; set; }
    private Text TimerText { get; set; }

    private void Update()
    {
        if (!TimerIsRunning) return;
        
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            DisplayTime(TimeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            TimeRemaining = 0;
            TimerIsRunning = false;
        }
    }

    public void Play()
    {
        TimerIsRunning = true;
    }

    public void Pause()
    {
        TimerIsRunning = false;
    }

    public void ResetTimer()
    {
        TimeRemaining = InitialDuration;
        TimerIsRunning = false;
    }

    private static void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // TimerText.text = $"{minutes:00}:{seconds:00}";
        Debug.Log($"{minutes:00}:{seconds:00}");
    }
}
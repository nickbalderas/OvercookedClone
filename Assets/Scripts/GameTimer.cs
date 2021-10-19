using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float InitialDuration { get; set; }
    private float TimeRemaining { get; set; }
    
    public TextMeshProUGUI TimerText { get; set; }

    public Action HandleTimerExpiration;

    private AudioSource _gameAudio;
    public AudioClip finalCountdownAudioClip;

    private void Awake()
    {
        _gameAudio = GameObject.Find("Game Manager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.timeScale == 0 || TimeRemaining == 0) return;

        if (TimeRemaining > 0)
        {
            if (TimeRemaining <= 3) _gameAudio.PlayOneShot(finalCountdownAudioClip);
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

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (TimerText)
        {
            TimerText.text = $"{minutes:0}:{seconds:00}";
        }
    }

    public void DestroyTimer()
    {
        Destroy(gameObject);
    }
}
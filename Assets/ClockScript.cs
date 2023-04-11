using System;
using UnityEngine;

/// <summary>
/// Keeps track of time and rotates the clock hands accordingly.
/// When given a time, it will give an output when that time has reached.
/// </summary>
public class ClockScript : MonoBehaviour
{ 
    [SerializeField] private Transform _hoursTransform;
    [SerializeField] private Transform _minutesTransform;
    [SerializeField] private Transform _secondsTransform;
    
    [SerializeField] private int _gameTimeInMinutes;
    [SerializeField] private int _checkpointTimeInMinutes;
    
    private DateTime _startTime;
    private DateTime _startingTime;
    private bool _isTimerActive;
    private bool _hasReachedCheckpoint;

    const float
        DEGREES_PER_HOUR = 30f,
        DEGREES_PER_MINUTE = 6f,
        DEGREES_PER_SECOND = 6f;


    private void Awake()
    {
        _startingTime = new DateTime(2023, 12, 1, 11, 60 - _gameTimeInMinutes, 0);
        StartCount();
    }

    private void StartCount()
    {
        _startTime = DateTime.Now;
        _isTimerActive = true;
    }

    private void Update()
    {
        if (!_isTimerActive) return;
        UpdateContinuous();
    }
    

    private void UpdateContinuous()
    {
        DateTime time = DateTime.Now;
        
        TimeSpan timeElapsed = time - _startTime;
        if(timeElapsed.TotalMinutes > _checkpointTimeInMinutes && !_hasReachedCheckpoint)
        {
            //Checkpoint reached
            Debug.Log("Checkpoint reached");
            _hasReachedCheckpoint = true;
        }
        
        if(timeElapsed.TotalMinutes > _gameTimeInMinutes)
        {
            //Time is up
            Debug.Log("Time is up");
            return;
        }
        
        DateTime timeToDisplay = _startingTime + timeElapsed;
        
        _hoursTransform.localRotation =
            Quaternion.Euler(0f, timeToDisplay.Hour * DEGREES_PER_HOUR, 0f);
        _minutesTransform.localRotation =
            Quaternion.Euler(0f, timeToDisplay.Minute * DEGREES_PER_MINUTE, 0f);
        _secondsTransform.localRotation =
            Quaternion.Euler(0f, timeToDisplay.Second * DEGREES_PER_SECOND, 0f);
    }
}
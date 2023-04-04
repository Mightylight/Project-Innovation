using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class MinigameState : MonoBehaviour
{
    [SerializeField] public GameObject hintPosition;
    [SerializeField] private float _timeInSeconds;
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
}

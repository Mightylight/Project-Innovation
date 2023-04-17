using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class MinigameState : MonoBehaviour
{
    [SerializeField] public List<GameObject> hintObjects;
    [SerializeField] private float _timeInSeconds;
    [SerializeField] public Material _paperMaterial;
    
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
}

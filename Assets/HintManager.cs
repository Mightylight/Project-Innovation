using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Minigames.LighterMinigame;
using UnityEngine;

public class HintManager : MonoBehaviour
{

    private void Start()
    {
        MinigameFSM.Instance.OnStateChanged += OnStateChanged;
    }


    private void OnStateChanged(MinigameState pState)
    {
        if (pState._paperMaterial == null) return;
        
        gameObject.GetComponent<Renderer>().material = pState._paperMaterial;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PaperManager : MonoBehaviour
{
    MinigameFSM miniFSM;
    bool firstTime = true;

    private void Start()
    {
        miniFSM = MinigameFSM.Instance;
    }

    public void OnMovement()
    {
        if(firstTime && miniFSM.CurrentState is ReadStartPaperMinigameState)
        {
            firstTime = false;
            miniFSM.NextState();
        }
    }
}

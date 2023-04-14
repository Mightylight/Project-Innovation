using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PaperManager : MonoBehaviour
{
    MinigameFSM miniFSM;
    bool firstTime = true;
    bool isClient;

    private void Start()
    {
        isClient = NetworkManager.Singleton.IsClient;
        if (isClient) return;
        //miniFSM = MinigameFSM.Instance;
    }

    public void OnMovement()
    {
        if (isClient) return;
        if (firstTime && MinigameFSM.Instance.CurrentState is ReadStartPaperMinigameState)
        {
            firstTime = false;
            MinigameFSM.Instance.NextState();
        }
    }
}

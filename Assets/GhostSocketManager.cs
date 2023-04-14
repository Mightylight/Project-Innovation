using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSocketManager : MonoBehaviour
{

    bool firstTime = true;

    public void OnGhostEnter()
    {
        if (firstTime)
        {
            MinigameFSM.Instance.NextState();
            firstTime = false;
        }
    }
}

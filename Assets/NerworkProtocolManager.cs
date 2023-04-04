using _Scripts.Minigames;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NerworkProtocolManager : NetworkBehaviour
{
    private static NerworkProtocolManager _instance;
    public static NerworkProtocolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("NerworkProtocolManager is null");
            }

            return _instance;
        }
    }

    [ServerRpc]
    public void RequestClueServerRpc()
    {
        MinigameFSM.Instance.GetClue();
    }


    [ClientRpc]
    public void ClueNotReadyClientRpc()
    {
        CanvasManager.Instance.PhoneConsoleMessage("Clue not ready");
        //Do stuff on clien
    }




}

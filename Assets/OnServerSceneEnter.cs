using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnServerSceneEnter : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.StartServer();
        VRPlayerManager.Instance.OnVRSceneLoaded();
    }

    void Update()
    {
        
    }
}

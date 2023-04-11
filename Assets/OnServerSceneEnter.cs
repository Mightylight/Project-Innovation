using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnServerSceneEnter : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.StartServer();
    }

    void Update()
    {
        
    }
}

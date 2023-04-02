using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkExample : MonoBehaviour
{
    [SerializeField] GameObject ServerNetworkExample;

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
    }

    private void OnServerStarted()
    {
        Instantiate(ServerNetworkExample).GetComponent<NetworkObject>().Spawn();

    }
}

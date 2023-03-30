using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.NetCode
{
    public class TestServerScript : MonoBehaviour
    {
        [SerializeField] private NetworkManager _networkManager;
        [SerializeField] private Transform _serverPlayerSpawner;
        
        
        private void Start()
        {
            Debug.Log("Starting server");
            _networkManager.StartHost();
            _networkManager.OnClientConnectedCallback += OnClientConnected;
        }

        private void OnClientConnected(ulong pObj)
        {
            _serverPlayerSpawner.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}

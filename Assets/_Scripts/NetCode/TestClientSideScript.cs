using Unity.Netcode;
using UnityEngine;

namespace _Scripts.NetCode
{
    public class TestClientSideScript : MonoBehaviour
    {
        [SerializeField] private NetworkManager _networkManager;
    
        private void Start()
        {
            Debug.Log("Starting client");
            _networkManager.StartClient();
        }
    }
}

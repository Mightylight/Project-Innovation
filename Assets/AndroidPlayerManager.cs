using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class AndroidPlayerManager : MonoBehaviour
{
    [SerializeField] GameObject androidPlayerObjectPrefab;
    [SerializeField] Transform androidPlayerSpawnPos;


    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    public void OnClientConnected(ulong clientID)
    {
        Debug.Log($"A new client connected with id {clientID}" );
        InitializeAndroidPlayerObject(clientID);
    }

    public void InitializeAndroidPlayerObject(ulong clientID)
    {
        GameObject obj = Instantiate(androidPlayerObjectPrefab, androidPlayerSpawnPos.position,androidPlayerSpawnPos.rotation);
        obj.GetComponent<NetworkObject>().Spawn();
        obj.GetComponent<AndroidPlayerObjectManager>().InitializeAndroidPlayerEye(clientID);

        //ClientRpcParams clientRpcParams = new ClientRpcParams
        //{
        //    Send = new ClientRpcSendParams
        //    {
        //        TargetClientIds = new ulong[] { clientID }
        //    }
        //};
        //obj.GetComponent<AndroidPlayerObjectManager>().InitializeAndroidPlayerEyeClientRpc(clientRpcParams);
    }

}

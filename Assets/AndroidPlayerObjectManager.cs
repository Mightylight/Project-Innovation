using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AndroidPlayerObjectManager : NetworkBehaviour
{
    [SerializeField] GameObject androidEyePrefab;

    public void InitializeAndroidPlayerEye(ulong clientId)
    {
        GameObject eye = Instantiate(androidEyePrefab, transform);
        eye.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        eye.transform.parent = transform;

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { (ulong)clientId }
            }
        };

        SetupEyeOnClientRpc(clientRpcParams);
       
    }

    [ClientRpc]
    public void SetupEyeOnClientRpc(ClientRpcParams clientRpcParams = default)
    {
        this.GetComponentInChildren<AndroidEyeController>().Init();
    }



}

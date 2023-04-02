using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AndroidPlayerObjectManager : NetworkBehaviour
{
    [SerializeField] GameObject androidEyePrefab;
    //This one is related to an android, but the server is the owner as they can pick it up. Storing the id so we can delete it from the server when the client disconnects
    ulong objOwner;

    public void InitializeAndroidPlayerEye(ulong clientId)
    {
        objOwner = clientId;
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

    //TODO: When the related client leaves, dedespawn (?) the object

}

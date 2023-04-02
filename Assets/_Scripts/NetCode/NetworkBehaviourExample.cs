using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkBehaviourExample : NetworkBehaviour
{
    ///A small example made to help create ServerRpc and clientRpc

    
    ///Instead of start, we can do OnNetworkSpawn within an NetworkBehaviour
    public override void OnNetworkSpawn()
    {
        PingServerRpc(10);
        PongClientRpc(12, "pong!");
    }


    /// <summary>
    /// Send a message to the server, the param will make it so any client can send the message.
    /// </summary>
    /// <param name="pingCount"></param>
    [ServerRpc(RequireOwnership = false)]
    public void PingServerRpc(int pingCount)
    {
        Debug.Log($"Somenumber {pingCount}");
    }

    /// <summary>
    /// Will send a message to ALL clients/is executed in all clients.
    /// </summary>
    [ClientRpc]
    void PongClientRpc(int somenumber, string sometext)
    {
        Debug.Log($"Somenumber: {somenumber} and text : {sometext}");
    }


    private void DoSomethingServerSide(int clientId)
    {
        // If isn't the Server/Host then we should early return here!
        if (!IsServer) return;


        // NOTE! In case you know a list of ClientId's ahead of time, that does not need change,
        // Then please consider caching this (as a member variable), to avoid Allocating Memory every time you run this function
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { (ulong)clientId }
            }
        };

        // Let's imagine that you need to compute a Random integer and want to send that to a client
        const int maxValue = 4;
        int randomInteger = Random.Range(0, maxValue);
        DoSomethingClientRpc(randomInteger, clientRpcParams);
    }

    [ClientRpc]
    private void DoSomethingClientRpc(int randomInteger, ClientRpcParams clientRpcParams = default)
    {
        if (IsOwner) return;

        // Run your client-side logic here!!
        Debug.LogFormat("GameObject: {0} has received a randomInteger with value: {1}", gameObject.name, randomInteger);
    }


}

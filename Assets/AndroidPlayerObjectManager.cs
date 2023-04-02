using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class AndroidPlayerObjectManager : NetworkBehaviour
{
    [SerializeField] GameObject androidEyePrefab;

    public void InitializeAndroidPlayerEye(ulong clientId)
    {
        GameObject eye = Instantiate(androidEyePrefab, transform);
        eye.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        eye.transform.parent = transform;
        //eye.GetComponent<NetworkObject>().Spawn();
        // NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation);

    }



}

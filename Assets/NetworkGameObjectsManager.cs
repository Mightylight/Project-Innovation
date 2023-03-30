using _Scripts;
using _Scripts.Networking.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameObjectsManager : MonoBehaviour
{
    private static NetworkGameObjectsManager instance;
    public static NetworkGameObjectsManager Instance
    {
        get {
            if (instance == null)
            {
                Debug.LogError("NetworkGameObjectsManager is null!");
                return null;
            }
            return instance; 
        }
    }
    [SerializeField] bool syncData = true;
    [SerializeField] float syncRefreshRate = 0.25f;

    [SerializeField] List<NetworkGameObject> networkGameObjects = new List<NetworkGameObject>();
    [SerializeField] List<NetworkGameObject> changedNetworkObjects = new();

    [SerializeField] bool isClient = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        if (!isClient) StartCoroutine(SyncTimer());

    }

    public void AddChangedNetworkObject(NetworkGameObject newNetworkObj)
    {
        int index = changedNetworkObjects.IndexOf(newNetworkObj);
        if (index >= 0) changedNetworkObjects[index] = newNetworkObj;
        else changedNetworkObjects.Add(newNetworkObj);
    }


    public void AddNetworkObject(NetworkGameObject newNetworkObj)
    {
        networkGameObjects.Add(newNetworkObj);
        newNetworkObj.SetNetworkObjectID(networkGameObjects.Count -1);
    }

    public void SendSyncObjectsRequest()
    {
        SerializableGameObjectList serializableGameObjectList = new SerializableGameObjectList();

        serializableGameObjectList.gameObjects = new();
        foreach(NetworkGameObject networkObject in changedNetworkObjects)
        {
            serializableGameObjectList.gameObjects.Add(networkObject.GetSerializeGameObject());
        }
        TCPServerSide.Instance.SendMessageToAll(serializableGameObjectList);
        changedNetworkObjects.Clear();
    }
    public void HandleSyncObjectsRequest(SerializableGameObjectList serializableGameObjectList)
    {
        foreach(SerializableGameObject networkGameObject in serializableGameObjectList.gameObjects)
        {
            changedNetworkObjects[networkGameObject.networkObjectID].Synchronize(networkGameObject);
        }
    }

    IEnumerator SyncTimer()
    {
        while (syncData)
        {
            yield return new WaitForSeconds(syncRefreshRate);
            SendSyncObjectsRequest();
        }
    }
}

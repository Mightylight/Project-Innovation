using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameObject : MonoBehaviour
{
    int networkObjectID;
    NetworkGameObjectsManager manager;

    private void Start()
    {
        manager = NetworkGameObjectsManager.Instance;
        manager.AddNetworkObject(this);
    }

    void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            manager.AddChangedNetworkObject(this);
            transform.hasChanged = false;
        }

    }

    public void SetNetworkObjectID(int newID)
    {
        networkObjectID = newID;
    }

    public void Synchronize(SerializableGameObject data)
    {
        transform.localPosition = data._transformPosition;
        transform.localRotation = data._transformRotation;
    }

    public SerializableGameObject GetSerializeGameObject()
    {
        SerializableGameObject serializableGameObject = new SerializableGameObject();
        serializableGameObject.networkObjectID = networkObjectID;
        serializableGameObject._transformPosition = transform.position;
        serializableGameObject._transformRotation = transform.rotation;
        return serializableGameObject;
    }


}

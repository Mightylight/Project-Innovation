using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DummyNetworkObject : MonoBehaviour
{
    [SerializeField] GameObject networkPrefabToSpawn;
    [Tooltip("Enable this to hide the object in the server")]
    [SerializeField] bool clientOnly = false;
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnNetworkObject;
    }

    void SpawnNetworkObject()
    {
        //TODO: nullcheck
        GameObject obj = Instantiate(networkPrefabToSpawn);
        obj.GetComponent<NetworkObject>().Spawn();
        obj.transform.position = transform.position;
        if (clientOnly) HideObject(obj);
        ObjectTracker tracker = obj.GetComponent<ObjectTracker>();
        if (tracker != null) tracker.SetTrackerObj(this.transform.parent);

        Destroy(this.gameObject);
    }
    void HideObject(GameObject obj)
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        // Loop through all the children and disable their mesh renderer
        foreach (Transform child in children)
        {
            renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }
}

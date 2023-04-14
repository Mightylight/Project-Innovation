using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyNetworkObject : MonoBehaviour
{
    [SerializeField] GameObject networkPrefabToSpawn;
    [Tooltip("Enable this to hide the object in the server")]
    [SerializeField] bool clientOnly = false;


    [SerializeField] bool copyPosition = true;
    [SerializeField] bool copyRotation = true;
    [SerializeField] bool copyScale = true
        ;
    void Start()
    {
        if(NetworkManager.Singleton == null)
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;
            return;
        }
        NetworkManager.Singleton.OnServerStarted += SpawnNetworkObject;
    }

    void ChangedActiveScene(Scene current, Scene next)
    {
        NetworkManager.Singleton.OnServerStarted += SpawnNetworkObject;
    }

    void SpawnNetworkObject()
    {
        //TODO: nullcheck
        GameObject obj = Instantiate(networkPrefabToSpawn);
        obj.GetComponent<NetworkObject>().Spawn();
        if(copyPosition)obj.transform.position = transform.position;
        if(copyRotation)obj.transform.rotation= transform.rotation;
        if(copyScale)obj.transform.localScale = transform.lossyScale;
        if (clientOnly) HideObject(obj);
        ObjectTracker tracker = obj.GetComponent<ObjectTracker>();
        if (tracker != null) tracker.SetTrackerObj(this.transform.parent);
        SuperHelper.DestroyChildren(this.transform);
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

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Important items we need to save and restore, if they somehow end up outside of the map.
/// </summary>
public class RestorationBackup : MonoBehaviour
{

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;

    private Rigidbody rb;
    private Vector3 velocity;


    private void Awake()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            Destroy(this);//We only need to do this on the server, save some resources on the mobile.
            return;
        }
        rb = transform.GetComponent<Rigidbody>();
        CreateBackup();

    }

    public void CreateBackup()
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
        if (rb != null) velocity = rb.velocity;
    }

    public void LoadBackup()
    {
        try{
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;

            if (rb != null)
            {
                rb.velocity = velocity;
            }
        }
        catch
        {
            Debug.LogError($"something wrong with restoring {gameObject.name} to orignal position");
        }
       
    }
}

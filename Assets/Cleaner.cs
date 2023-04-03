using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] float rayDistance = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit raycast, rayDistance);
        if (raycast.collider == null) return;
        CleanableSurface cleanableSurface = raycast.collider.GetComponent<CleanableSurface>();
        if (cleanableSurface != null) cleanableSurface.CleanObject(raycast.textureCoord);
        //CleanObject(raycast.textureCoord);
    }
}

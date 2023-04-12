using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Object tracker can be used to follow an object.
/// It will take the same place as the other object
/// It's main purpose is to be an alternative to parenting a networkobject to a non-network object
/// </summary>
public class ObjectTracker : MonoBehaviour
{
    [SerializeField] Transform objectToTrack;

    public void SetTrackerObj(Transform _objectToTrack)
    {
        objectToTrack = _objectToTrack;
    }

    void Update()
    {
        if (objectToTrack != null) TrackObject();
    }
    void TrackObject()
    {
        this.transform.position = objectToTrack.position;
        this.transform.rotation = objectToTrack.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGyroControls : MonoBehaviour
{
    Quaternion offset;
    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Start()
    {
        offset = transform.localRotation * Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    }

    void Update()
    {
        transform.localRotation = offset * GyroToUnity(Input.gyro.attitude);
        Debug.Log(Input.gyro.attitude);
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGyroControls : MonoBehaviour
{
    Quaternion offset;
    Quaternion original;
    void Awake()
    {
        Input.gyro.enabled = true;
        original = transform.localRotation;
    }

    void Start()
    {
        ResetOffset();
        //offset = transform.localRotation * Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) ResetOffset();
        transform.localRotation = offset * GyroToUnity(Input.gyro.attitude);
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public void ResetOffset()
    {
        transform.localRotation = original;
        offset = transform.localRotation * Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
        Debug.Log("Gyro offset has been reset!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidEyeController : MonoBehaviour
{
    [SerializeField] GameObject androidCameraPrefab;

    public void Init()
    {
        //Note that we don't actually move the camera around, but instead move the "eye" gameobject with the camera as child
        gameObject.AddComponent<CameraTouchControls>();
        //gameObject.AddComponent<AndroidCameraController>();//TODO: Create android camera controller, that can be used if gyroscope is disabled!
        Instantiate(androidCameraPrefab, transform);//Add the camera ment for the android, as child.

    }
}

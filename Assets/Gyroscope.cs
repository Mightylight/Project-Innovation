using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    private bool _gyroEnabled; 
    private UnityEngine.Gyroscope _gyro;
    private GameObject _gyroControl;
    private Quaternion _rot;
 
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
 
        _gyroControl = new GameObject("Gyro Control");
        _gyroControl.transform.position = transform.position; 
        transform.SetParent(_gyroControl.transform); //parents the object to an empty control object
        _gyroEnabled = EnableGyro();
    }
    private bool EnableGyro()
    {
        if (!SystemInfo.supportsGyroscope) return false;
        
        _gyro = Input.gyro;
        _gyro.enabled = true;
 
        _gyroControl.transform.rotation = Quaternion.Euler(90f, -90f, 0f); //These offset values are essential for the gyroscope to orientate itself correctly
        _rot = new Quaternion(0, 0, 1, 0);
 
        return true;
    }
    private void Update()
    {
        Quaternion rotMin = Quaternion.Euler(new Vector3(0, 0, 0));
 
        Quaternion rotation = transform.rotation; //Values for locking the rotation of the gyro
 
        if (_gyroEnabled)
        {
            transform.localRotation = _gyro.attitude * _rot;
        }
 
        if (rotation.y < rotMin.y)
        {
            transform.eulerAngles = Vector3.zero; //Doesnt allow rotation values to be in the negative
        }
    }
}

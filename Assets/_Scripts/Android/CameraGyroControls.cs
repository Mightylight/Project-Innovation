using UnityEngine;

public class CameraGyroControls : MonoBehaviour
{
    Quaternion offset;
    Quaternion original;
    private UnityEngine.Gyroscope _gyro;
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        original = transform.rotation;
        EnableGyro();
        CanvasManager.Instance.fixButton.onClick.AddListener(() => EnableGyro());
    }

    public void EnableGyro()
    {
        if (!SystemInfo.supportsGyroscope) return;

        _gyro = Input.gyro;
        _gyro.enabled = true;
        ResetOffset();
    }

    public void DisableGyro()
    {
        _gyro.enabled = false;
    }


    //void Start()
    //{
    //    //ResetOffset();
    //    //offset = transform.localRotation * Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    //}

    void Update()
    {
        //if(!_gyroEnabled) return;
        //if (Input.GetKeyDown(KeyCode.V)) ResetOffset();
        transform.rotation = offset * GyroToUnity(_gyro.attitude);
        //transform.rotation = new Quaternion(transform.rotation.x + 10, 10, 10, 1);
        //CanvasManager.Instance.PhoneConsoleMessage($"{_gyro.attitude}");
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public void ResetOffset()
    {
        transform.rotation = original;
        offset = transform.rotation * Quaternion.Inverse(GyroToUnity(_gyro.attitude));
        Debug.Log("Gyro offset has been reset!");
        CanvasManager.Instance.PhoneConsoleMessage($"Gyro offset has been reset!{offset}");
    }
}

using UnityEngine;

public class CameraGyroControls : MonoBehaviour
{
    Quaternion offset;
    Quaternion original;
    private UnityEngine.Gyroscope _gyro;
    [SerializeField] bool useGyro = true;
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
        useGyro = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float rotationSpeed = 0.5f;
            offset *= Quaternion.Euler(-touchDeltaPosition.y * rotationSpeed, touchDeltaPosition.x * rotationSpeed, 0);
        }
        if (useGyro) transform.rotation = offset * GyroToUnity(_gyro.attitude);
        //TODO: Add mobile screen support, so the player can also look around in 360 degrees by swiping on the screen.
        //Note that the rotation from the touch should be added to the offset so the gyroscope AND touchcontrols can be used at the same time.


    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public void ResetOffset()
    {
        transform.rotation = original;
        offset = transform.rotation * Quaternion.Inverse(GyroToUnity(_gyro.attitude));
        //Debug.Log("Gyro offset has been reset!");
        CanvasManager.Instance.PhoneConsoleMessage($"Gyro offset has been reset!{offset}"); 
    }

}

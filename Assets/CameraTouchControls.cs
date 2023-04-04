using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControls : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 5.0f;
    public float ySpeed = 5.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public float zoomRate = 10.0f;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    private Vector3 FirstPosition;
    private Vector3 SecondPosition;
    private Vector3 delta;
    private Vector3 lastOffset;
    private Vector3 lastOffsettemp;



    Quaternion offset;
    Quaternion original;
    private UnityEngine.Gyroscope _gyro;
    [SerializeField] bool useGyro = true;

    Quaternion gyroRotation;

    private void Start()
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



    void LateUpdate()
    {
        HandleZoom();
        HandleOrbit();

        if (useGyro) gyroRotation = offset * GyroToUnity(_gyro.attitude);
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0) * gyroRotation;
        currentRotation = transform.rotation;
        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(1))
        {
            FirstPosition = Input.mousePosition;
            lastOffset = targetOffset;
        }

        if (Input.GetMouseButton(1))
        {
            SecondPosition = Input.mousePosition;
            delta = SecondPosition - FirstPosition;
            targetOffset = lastOffset + transform.right * delta.x * 0.003f + transform.up * delta.y * 0.003f;
        }

        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        //position = target.position - (rotation * Vector3.forward * currentDistance);
        //position = position - targetOffset;
        //transform.position = position;

        //rotation = 

    }

    void HandleZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;
            desiredDistance += deltaMagDiff * Time.deltaTime * zoomRate * 0.0025f * Mathf.Abs(desiredDistance);
        }
    }

    void HandleOrbit()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchPosition = Input.GetTouch(0).deltaPosition;
            xDeg += touchPosition.x * xSpeed * 0.002f;
            yDeg -= touchPosition.y * ySpeed * 0.002f;
           // yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        return Mathf.Clamp(angle, min, max);
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cameraPositions;
    [SerializeField] private CameraControls _cameraControls;
    

    private Camera _camera;
    private Transform _cameraTransform;
    
    void Start()
    {
        _camera = Camera.main;
        _cameraTransform = _camera.transform;
        _cameraTransform.position = _cameraPositions[0].transform.position;
        _cameraTransform.rotation = _cameraPositions[0].transform.rotation;
    }
    
    public void SetCamera(int pCameraIndex)
    {
        _cameraTransform.position = _cameraPositions[pCameraIndex].transform.position;
        _cameraTransform.rotation = _cameraPositions[pCameraIndex].transform.rotation;
    }
}

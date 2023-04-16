using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpinning : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _rotation;
    private void Start()
    {
        _rotation = Random.insideUnitSphere;
    }
    
    
    private void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }
    
}

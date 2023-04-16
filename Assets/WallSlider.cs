using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class WallSlider : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _duration;

    private Vector3 _originalPos;

    private void Start()
    {
        _originalPos = transform.position;
        //SlideWall();
    }


    public void SlideWall()
    {
        transform.DOMove(_targetPos, _duration).SetEase(Ease.InOutQuad);
        TryGetComponent(out AudioSource source);
        source.Play();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WallSlider : NetworkBehaviour
{


    [SerializeField] AnimationCurve curve;

    [SerializeField] Vector3 originalPos;
    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed;
    float current, target;




    public void SlideWall()
    {
        current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(originalPos, targetPos, curve.Evaluate(current));
    }

    private void Update()
    {
        SlideWall();
    }
}

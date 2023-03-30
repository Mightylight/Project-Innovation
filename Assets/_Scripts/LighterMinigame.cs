using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterMinigame : MonoBehaviour
{
    private GameObject _candle;
    [SerializeField] private GameObject _lighter;
    

    // Start is called before the first frame update
    void Start()
    {
        _candle = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _lighter) return;
        _candle.GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(BurnCandle());
    }


    private IEnumerator BurnCandle()
    {
        yield return new WaitForSeconds(1);
        _candle.GetComponent<Renderer>().material.color = Color.black;
        yield return new WaitForSeconds(1);
        Destroy(_candle);
    }
}
